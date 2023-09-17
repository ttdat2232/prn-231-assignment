using Application.Dtos;
using Application.Dtos.Creates;

namespace Application.Interfaces
{
    public interface IRentingService
    {
        Task<RentingTransactionDto> CreateRentingTransactionAsync(int userId, List<CreateRentingDetailDto> dtos);
        Task<RentingTransactionDto> GetTransactionByUserIdAndTractionId(int userId, int transactionId);
    }
}
