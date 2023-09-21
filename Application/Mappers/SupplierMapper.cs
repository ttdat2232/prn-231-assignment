using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers
{
    public static class SupplierMapper
    {
        public static SupplierDto ToDto(Supplier s)
        {
            return new SupplierDto
            {
                SupplierId = s.SupplierId,
                SupplierName = s.SupplierName,
            };
        }
    }
}
