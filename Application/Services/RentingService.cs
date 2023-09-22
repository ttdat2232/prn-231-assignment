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
                var existedCar = await unitOfWork.Cars.GetAsync(expression: c => c.CarId == rentingDetailDto.CarId && c.CarStatus == 1)
                    .ContinueWith(t => t.Result.Values.Any() ? t.Result.Values.First() : throw new NotFoundException<CarInformation>(rentingDetailDto.CarId, GetType()));
                await unitOfWork.RentingDetails.GetAsync(
                    expression: RentingDetailExpression.GetRentingDetailFromToOfCar(from, to, existedCar.CarId),
                    takeAll: true)
                    .ContinueWith(t =>
                    {
                        if (t.Result.Values.Any())
                            throw new ConflictExpcetion($"{existedCar.CarName} was rented");
                    });
                TimeSpan timeSpan = new DateTime(to.Year, to.Month, to.Day) - new DateTime(from.Year, from.Month, from.Day);
                var totalDays = timeSpan.TotalDays == 0 ? 1 : timeSpan.TotalDays;
                var price = (decimal)totalDays * existedCar.CarRentingPricePerDay ?? 0;
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

        public async Task<List<StatictisResult>> GetStatictisResult(DateTime start, DateTime end)
        {
            if (start > end)
                throw new ConflictExpcetion("start date cannot later than end date");
            var result = await unitOfWork.RentingTransactions.GetAsync(
                expression: r => r.RentingDate >= start && r.RentingDate < end, 
                takeAll: true,
                orderBy: q => q.OrderByDescending(r => r.RentingDate))
                .ContinueWith(t => t.Result.Values);
            var existed = new Dictionary<int, StatictisResult>();
            foreach(var item in result)
            {
                if(item.RentingDate.HasValue)
                {
                    if(existed.TryGetValue(item.RentingDate.Value.Year, out var value))
                    {
                        if (value.MonthIncomes.TryGetValue(item.RentingDate.Value.Month, out var income))
                        {
                            value.MonthIncomes[item.RentingDate.Value.Month] = income + item.TotalPrice ?? 0;
                        }
                        existed[item.RentingDate.Value.Year] = value;
                    }
                    else
                    {
                        var newValues = new StatictisResult { Year = item.RentingDate.Value.Year };
                        newValues.MonthIncomes[item.RentingDate.Value.Month] = item.TotalPrice ?? 0;
                        existed.Add(item.RentingDate.Value.Year, newValues);
                    }
                }
            }
            return existed.Values.ToList();
        }

        public async Task<RentingTransactionDto> GetTransactionByUserIdAndTractionId(int userId, int transactionId)
        {
            return RentingTransactionMapper.ToDto(await unitOfWork.RentingTransactions.GetByIdAsync(userId, transactionId));
        }

        public async Task<List<RentingTransactionDto>> GetTransactionsOfUser(int userId)
        {
            return await unitOfWork.RentingTransactions.GetAsync(expression: r => r.CustomerId == userId, takeAll: true)
                .ContinueWith(t => t.Result.Values.Select(r => RentingTransactionMapper.ToDto(r)).ToList());
        }
    }
}
