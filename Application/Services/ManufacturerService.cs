using Application.Dtos;
using Application.Interfaces;
using Application.Mappers;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Application.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IUnitOfWork unitOfWork;

        public ManufacturerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<ManufacturerDto>> GetManufacturerAsync()
        {
            var result = await unitOfWork.Manufacturers.GetAsync(takeAll: true)
                .ContinueWith(t => new PaginationResult<ManufacturerDto>
                {
                    PageIndex = t.Result.PageIndex,
                    PageSize = t.Result.PageSize,
                    TotalCount = t.Result.TotalCount,
                    Values = t.Result.Values.Select(m => ManufacturerMapper.ToDto(m)).ToList()
                });
            return result;
        }
    }
}
