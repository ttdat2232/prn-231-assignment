using Domain.Entities;
using Domain.Interfaces.Repositories;
using Repositories.Base;
using Repositories.Data;

namespace Repositories
{
    public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(FUCarRentingManagementContext context) : base(context)
        {
        }
    }
}
