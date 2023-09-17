using Domain.Entities;
using Domain.Interfaces.Repositories;
using Repositories.Base;
using Repositories.Data;

namespace Repositories
{
    public class RentingDetailRepository : Repository<RentingDetail>, IRentingDetailRepository
    {
        public RentingDetailRepository(FUCarRentingManagementContext context) : base(context)
        {
        }
    }
}
