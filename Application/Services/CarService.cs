using Application.Dtos;
using Application.Dtos.Creates;
using Application.Dtos.Updates;
using Application.Exceptions;
using Application.Expressions;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models;
using System.Linq.Expressions;

namespace Application.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private Expression<Func<CarInformation, bool>> GenerateExpression(string? name = "", int[]? status = null)
        {
            var exps = new List<Expression<Func<CarInformation, bool>>>
            {
                CarExpression.CarContainsName(name),
                CarExpression.CarInStatuses(status),
            };
            var combined = exps[0];
            if (exps.Count == 1)
                return combined;
            foreach (var exp in exps.Skip(1))
            {
                combined = PredicateBuilder.And(combined, exp);
            }
            return combined;
        }

        public async Task<PaginationResult<CarInformationDto>> GetCarInformationAsync(
            int pageIndex = 0, 
            int pageSize = 10,
            string? name = "", 
            int[]? status = null)
        {
            var exp = GenerateExpression(name, status);
            var result = await unitOfWork.Cars.GetAsync(
                expression: exp, 
                orderBy: c => c.OrderByDescending(x => x.CarId),
                pageIndex: pageIndex,
                pageSize: pageSize);
            return new PaginationResult<CarInformationDto>
            {
                PageIndex = result.PageIndex,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                Values = result.Values.Select(c => CarInformationMapper.ToDto(c)).ToList()
            };
        }

        public async Task<CarInformationDto> AddCarInformationAsync(CreateCarInformationDto dto)
        {
            var entityToAdd = CarInformationMapper.ToEntity(dto);
            var existedManufacturer = await unitOfWork.Manufacturers.GetByIdAsync(new object[] {dto.ManufacturerId});
            entityToAdd.ManufacturerId = existedManufacturer != null ? dto.ManufacturerId : throw new NotFoundException<Manufacturer>(dto.ManufacturerId, GetType());
            var existedSupplier = await unitOfWork.Suppliers.GetByIdAsync(new object[] { dto.ManufacturerId });
            entityToAdd.SupplierId = existedSupplier != null ? dto.SupplierId : throw new NotFoundException<Supplier>(dto.SupplierId, GetType());
            entityToAdd = await unitOfWork.Cars.AddAsync(entityToAdd);
            await unitOfWork.CompleteAsync();
            return CarInformationMapper.ToDto(entityToAdd);
        }

        public async Task<CarInformationDto> GetCarInformationByIdAsync(int id)
        {
            return CarInformationMapper.ToDto(await unitOfWork.Cars.GetByIdAsync(new object[] { id } ));
        }

        public async Task UpdateCarInformation(UpdateCarInformationDto dto)
        {
            var exitedCarInformation = await unitOfWork.Cars.GetByIdAsync(new object[] {dto.CarId});
            CarInformationMapper.ToEntity(dto, ref exitedCarInformation);
            unitOfWork.Cars.Update(exitedCarInformation);
            await unitOfWork.CompleteAsync();
        }

        public async Task DeleteCarInformation(int id)
        {
            var entityToDelete = await unitOfWork.Cars.GetByIdAsync(new object[] {id});
            if(entityToDelete.RentingDetails.Count > 0)
            {
                entityToDelete.CarStatus = 0;
                unitOfWork.Cars.Update(entityToDelete);
            }
            else
                unitOfWork.Cars.Delete(entityToDelete);
            await unitOfWork.CompleteAsync();
        }
    }
}
