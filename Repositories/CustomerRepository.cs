using Application.Exceptions;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Data;

namespace Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(FUCarRentingManagementContext context) : base(context)
        {
        }
        public override async Task<Customer> GetByIdAsync(object[] keys)
        {
            var id = (int)keys[0];
            var result = await context.Set<Customer>()
                .Include(c => c.RentingTransactions)
                .AsSplitQuery()
                .Where(c => c.CustomerId == id)
                .ToListAsync();
            return result.Any() ? result.First() : throw new NotFoundException<Customer>(keys, GetType());
        }
    }
}
