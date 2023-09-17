using Application.Exceptions;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Data;

namespace Repositories
{
    public class CarRepository : Repository<CarInformation>, ICarRepository
    {
        public CarRepository(FUCarRentingManagementContext context) : base(context)
        {
        }

        public override async Task<CarInformation> GetByIdAsync(object[] keys)
        {
            var result = await context.Set<CarInformation>()
                .Include(c => c.RentingDetails)
                .AsSplitQuery()
                .Where(c => c.CarId == (int)keys[0])
                .ToListAsync();
            return result.Count > 0 ? result[0] : throw new NotFoundException<CarInformation>(keys, GetType());
        }
    }
}
