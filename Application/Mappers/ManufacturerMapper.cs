using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers
{
    public static class ManufacturerMapper
    {
        public static ManufacturerDto ToDto(Manufacturer manufacturer)
        {
            return new ManufacturerDto { ManufacturerId = manufacturer.ManufacturerId, ManufacturerName = manufacturer.ManufacturerName };
        }
    }
}
