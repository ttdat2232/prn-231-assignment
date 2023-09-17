using Application.Dtos;
using Application.Dtos.Updates;
using Domain.Entities;

namespace Application.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(Customer customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerBirthday = customer.CustomerBirthday,
                CustomerName = customer.CustomerName,
                CustomerStatus = customer.CustomerStatus,
                Email = customer.Email,
                Telephone = customer.Telephone,
            };
        }

        public static void ToEntity(UpdateCustomerInformationDto dto, ref Customer existed)
        {
            if(dto.CustomerBirthday != null)
                existed.CustomerBirthday = dto.CustomerBirthday;
            if(dto.CustomerName != null)
                existed.CustomerName = dto.CustomerName;
        }
    }
}
