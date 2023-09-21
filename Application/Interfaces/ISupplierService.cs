using Application.Dtos;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ISupplierService
    {
        Task<PaginationResult<SupplierDto>> GetSuppliersAsync();
    }
}
