using Application.Dtos;
using Application.Dtos.Creates;
using Application.Exceptions;
using Application.Expressions;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public class RentingService : IRentingService
    {
        private readonly IUnitOfWork unitOfWork;

        public RentingService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<RentingTransactionDto> CreateRentingTransactionAsync(int userId, List<CreateRentingDetailDto> dtos)
        {
            if (dtos.Count == 0)
                throw new ConflictExpcetion("renting detail cannot be empty");
            var setCarId = new HashSet<int>();
            foreach (var item in dtos)
            {
                if (setCarId.Contains(item.CarId) is true)
                    throw new ConflictExpcetion($"Car with Id {item.CarId} has more than one in list");
                setCarId.Add(item.CarId);
            }
            var rentingTransaction = new RentingTransaction()
            {
                CustomerId = userId,
                RentingDate = DateTime.UtcNow,
                RentingStatus = 1,
                RentingDetails = new List<RentingDetail>(),
            };
            decimal totalPrice = 0;
            foreach(var rentingDetailDto in dtos)
            {
                var from = rentingDetailDto.StartDate;
                var to = rentingDetailDto.EndDate;
                if (from < DateTime.UtcNow)
                    throw new ConflictExpcetion($"'From' of car with Id {rentingDetailDto.CarId} date must be later than at the current");
                if (from > to)
                    throw new ConflictExpcetion("'From' date must be earlier than 'To' date");
                var existedCar = await unitOfWork.Cars.GetAsync(expression: c => c.CarId == rentingDetailDto.CarId)
                    .ContinueWith(t => t.Result.Values.Any() ? t.Result.Values.First() : throw new NotFoundException<CarInformation>(rentingDetailDto.CarId, GetType()));
                await unitOfWork.RentingDetails.GetAsync(
                    expression: RentingDetailExpression.GetRentingDetailFromToOfCar(from, to, existedCar.CarId),
                    takeAll: true)
                    .ContinueWith(t =>
                    {
                        if (t.Result.Values.Any())
                            throw new ConflictExpcetion($"Car {rentingDetailDto.CarId} was rented");
                    });
                TimeSpan timeSpan = to - from;
                var price = (decimal)timeSpan.TotalDays * existedCar.CarRentingPricePerDay ?? 0;
                totalPrice += price;
                rentingTransaction.RentingDetails.Add(new RentingDetail
                {
                    CarId = existedCar.CarId,
                    StartDate = from,
                    EndDate = to,
                    Price = price
                });
            }
            rentingTransaction.TotalPrice = totalPrice;
            rentingTransaction = await unitOfWork.RentingTransactions.AddAsync(rentingTransaction);
            await unitOfWork.CompleteAsync();
            return new RentingTransactionDto
            {
                RentingTransationId = rentingTransaction.RentingTransationId
            };
        }

        public async Task<RentingTransactionDto> GetTransactionByUserIdAndTractionId(int userId, int transactionId)
        {
            var result = await unitOfWork.RentingTransactions.GetAsync(
                expression: r => r.CustomerId == userId && r.RentingTransationId == transactionId,
                pageSize: 1,
                includeProperties: new string[] {nameof(RentingTransaction.RentingDetails)})
                .ContinueWith(t => t.Result.Values.Any() ? t.Result.Values[0] : throw new NotFoundException<RentingTransaction>(new { userId, transactionId}, GetType()));
            return RentingTransactionMapper.ToDto(result);
        }
    }
}
