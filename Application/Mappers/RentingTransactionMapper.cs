using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers
{
    public static class RentingTransactionMapper
    {
        public static RentingTransactionDto ToDto(RentingTransaction entity)
        {
            var result = new RentingTransactionDto()
            {
                CustomerId = entity.CustomerId,
                RentingDate = entity.RentingDate,
                RentingTransationId = entity.RentingTransationId,
                TotalPrice = entity.TotalPrice
            };
            if (entity.RentingDetails.Any())
                result.RentingDetails = entity.RentingDetails.Select(rd => RentingDetailMapper.ToDto(rd)).ToList();            
            return result;
        }
    }
}
