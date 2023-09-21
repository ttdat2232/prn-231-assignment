using Application.Dtos;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IManufacturerService
    {
        Task<PaginationResult<ManufacturerDto>> GetManufacturerAsync();
    }
}
