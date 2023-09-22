using Domain.Entities;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Interfaces.Repositories
{
    public interface IRentingTransactionRepository : IRepository<RentingTransaction>
    {
        Task<RentingTransaction> GetByIdAsync(int userId, int transactionId);
    }
}
