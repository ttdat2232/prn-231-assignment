using Application.Dtos;
using Application.Dtos.Creates;
using Application.Dtos.Updates;
using Domain.Models;

namespace Application.Interfaces
{
    public interface ICarService
    {
        Task<CarInformationDto> AddCarInformationAsync(CreateCarInformationDto dto);
        Task DeleteCarInformation(int id);
        Task<PaginationResult<CarInformationDto>> GetCarInformationAsync(
            int pageIndex = 0, 
            int pageSize = 10,
            string? name = "", 
            int[]? status = null);
        Task<CarInformationDto> GetCarInformationByIdAsync(int id);
        Task UpdateCarInformation(UpdateCarInformationDto dto);
    }
}
