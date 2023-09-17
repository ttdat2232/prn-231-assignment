using Domain.Entities;
using Domain.Interfaces.Repositories;
using Repositories.Base;
using Repositories.Data;

namespace Repositories
{
    public class RentingTransactionRepository : Repository<RentingTransaction>, IRentingTransactionRepository
    {
        public RentingTransactionRepository(FUCarRentingManagementContext context) : base(context)
        {
        }

        public override Task<RentingTransaction> AddAsync(RentingTransaction entity)
        {
            var maxId = context.Set<RentingTransaction>().Select(c => c.RentingTransationId).Max();
            entity.RentingTransationId = maxId + 1;
            return base.AddAsync(entity);
        }
    }
}
