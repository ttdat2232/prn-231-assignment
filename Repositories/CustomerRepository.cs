using Domain.Entities;
using Domain.Interfaces.Repositories;
using Repositories.Base;
using Repositories.Data;

namespace Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(FUCarRentingManagementContext context) : base(context)
        {
        }
    }
}
