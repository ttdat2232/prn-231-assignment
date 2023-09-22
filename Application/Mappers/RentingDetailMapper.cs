using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers
{
    public static class RentingDetailMapper
    {
        public static RentingDetailDto ToDto(RentingDetail entity)
        {
            var result = new RentingDetailDto
            {
                CarId = entity.CarId,
                EndDate = entity.EndDate,
                Price = entity.Price,
                RentingTransactionId = entity.RentingTransactionId,
                StartDate = entity.StartDate,
            };
            if (entity.Car != null)
                result.CarName = entity.Car.CarName;
            return result;
        }
    }
}
