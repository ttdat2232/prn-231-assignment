using Application.Dtos;
using Application.Dtos.Creates;

namespace Application.Interfaces
{
    public interface IRentingService
    {
        Task<RentingTransactionDto> CreateRentingTransactionAsync(int userId, List<CreateRentingDetailDto> dtos);
        Task<List<StatictisResult>> GetStatictisResult(DateTime start, DateTime end);
        Task<RentingTransactionDto> GetTransactionByUserIdAndTractionId(int userId, int transactionId);
        Task<List<RentingTransactionDto>> GetTransactionsOfUser(int userId);
    }
}
