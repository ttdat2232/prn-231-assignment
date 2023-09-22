using Application.Exceptions;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories.Base;
using Repositories.Data;

namespace Repositories
{
    public class RentingTransactionRepository : Repository<RentingTransaction>, IRentingTransactionRepository
    {
        public RentingTransactionRepository(FUCarRentingManagementContext context) : base(context)
        {
        }

        public async Task<RentingTransaction> GetByIdAsync(int userId, int transactionId)
        {
            var transaction = await context.Set<RentingTransaction>()
                .AsNoTracking()
                .Include(t => t.RentingDetails).ThenInclude(rd => rd.Car)
            .AsSplitQuery()
                .Where(t => t.RentingTransationId == transactionId && t.CustomerId == userId)
                .ToListAsync();
            return transaction.Count > 0 ? transaction.First() : throw new NotFoundException<RentingTransaction>(transaction, GetType());
        }
        public override Task<RentingTransaction> AddAsync(RentingTransaction entity)
        {
            var maxId = context.Set<RentingTransaction>().Select(c => c.RentingTransationId).Max();
            entity.RentingTransationId = maxId + 1;
            return base.AddAsync(entity);
        }
    }
}
