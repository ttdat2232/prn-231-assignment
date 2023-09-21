using Application.Dtos;
using Application.Interfaces;
using Application.Mappers;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Application.Services
{
    public class SupplierService: ISupplierService
    {
        private readonly IUnitOfWork unitOfWork;

        public SupplierService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<SupplierDto>> GetSuppliersAsync()
        {
            var result = await unitOfWork.Suppliers.GetAsync(takeAll: true)
                .ContinueWith(t => new PaginationResult<SupplierDto>
                {
                    PageIndex = t.Result.PageIndex,
                    PageSize = t.Result.PageSize,
                    TotalCount = t.Result.TotalCount,
                    Values = t.Result.Values.Select(s => SupplierMapper.ToDto(s)).ToList()
                });
            return result;
        }
    }
}
