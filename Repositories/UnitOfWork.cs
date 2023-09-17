using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;
        private bool disposed = false;
        private readonly ICarRepository carRepository;
        private readonly IManufacturerRepository manufacturerRepository; 
        private readonly ISupplierRepository supplierRepository;
        private readonly ICustomerRepository customerRepository;

        public ICarRepository Cars => carRepository;
        public IManufacturerRepository Manufacturers => manufacturerRepository;
        public ISupplierRepository Suppliers => supplierRepository;
        public ICustomerRepository Customers => customerRepository;

        public UnitOfWork(
            FUCarRentingManagementContext context,
            ICarRepository carRepository,
            IManufacturerRepository manufacturerRepository,
            ISupplierRepository supplierRepository,
            ICustomerRepository customerRepository)
        {
            this.context = context;
            this.carRepository = carRepository;
            this.manufacturerRepository = manufacturerRepository;
            this.supplierRepository = supplierRepository;
            this.customerRepository = customerRepository;
        }
        public int Complete()
        {
            return context.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                context.Dispose();
            }
            disposed = true;
        }
        public async ValueTask DisposeAsync()
        {
            await context.DisposeAsync();
            Dispose(false);
            GC.SuppressFinalize(this);
        }
    }
}
