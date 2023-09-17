using Application.Dtos;
using Application.Dtos.Creates;
using Application.Dtos.Updates;
using Application.Exceptions;
using Domain.Entities;

namespace Application.Mappers
{
    public static class CarInformationMapper
    {
        public static CarInformationDto ToDto(CarInformation entity)
        {
            var result = new CarInformationDto()
            {
                CarDescription = entity.CarDescription,
                CarName = entity.CarName,
                CarStatus = entity.CarStatus,
                CarId = entity.CarId,
                CarRentingPricePerDay = entity.CarRentingPricePerDay,
                FuelType = entity.FuelType,
                ManufacturerId = entity.ManufacturerId,
                NumberOfDoors = entity.NumberOfDoors,
                SeatingCapacity = entity.SeatingCapacity,
                SupplierId = entity.SupplierId,
                Year = entity.Year,
            };

            return result;
        }

        public static CarInformation ToEntity(CreateCarInformationDto dto)
        {
            var result = new CarInformation()
            {
                CarDescription = dto.CarDescription,
                CarName = dto.CarName,
                CarRentingPricePerDay = dto.CarRentingPricePerDay > 0 ? dto.CarRentingPricePerDay : throw new ConflictExpcetion($"Renting price/day must equal to or greater than 0"),
                CarStatus = dto.CarStatus,
            };

            if (dto.FuelType.HasValue)
                result.FuelType = dto.FuelType.Value.ToString();
            if (dto.NumberOfDoors.HasValue)
                result.NumberOfDoors = (int)dto.NumberOfDoors.Value;
            return result;
        }

        public static void ToEntity(UpdateCarInformationDto dto, ref CarInformation exitedCarInformation)
        {
            if(dto.ManufacturerId.HasValue)
                exitedCarInformation.ManufacturerId = dto.ManufacturerId.Value;
            if(dto.FuelType.HasValue)
                exitedCarInformation.FuelType = dto.FuelType.ToString();
            if(dto.NumberOfDoors.HasValue)
                exitedCarInformation.NumberOfDoors = (int)dto.NumberOfDoors.Value;
            if (dto.CarStatus.HasValue)
                exitedCarInformation.CarStatus = dto.CarStatus.Value;
            if (dto.CarDescription != null)
                exitedCarInformation.CarDescription = dto.CarDescription;
            if (dto.CarRentingPricePerDay.HasValue)
            {
                if (dto.CarRentingPricePerDay.Value > 0)
                    exitedCarInformation.CarRentingPricePerDay = dto.CarRentingPricePerDay.Value;
                else
                    throw new ConflictExpcetion($"Renting price/day must equal to or greater than 0");
            }
            if (dto.ManufacturerId.HasValue)
                exitedCarInformation.ManufacturerId = dto.ManufacturerId.Value;
            if(dto.SupplierId.HasValue)
                exitedCarInformation.SupplierId = dto.SupplierId.Value;
            exitedCarInformation.SeatingCapacity = dto.SeatingCapacity;
            exitedCarInformation.Year = dto.Year;
        }
    }
}
