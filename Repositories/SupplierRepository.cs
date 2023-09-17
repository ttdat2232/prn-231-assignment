using Domain.Entities;
using Domain.Interfaces.Repositories;
using Repositories.Base;
using Repositories.Data;

namespace Repositories
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(FUCarRentingManagementContext context) : base(context)
        {
        }
    }
}
