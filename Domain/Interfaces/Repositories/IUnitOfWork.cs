namespace Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        ICarRepository Cars { get; }
        IManufacturerRepository Manufacturers { get; }
        ISupplierRepository Suppliers { get; }
        ICustomerRepository Customers { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
