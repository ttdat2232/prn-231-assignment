using Application.Dtos;
using Application.Dtos.Updates;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<CustomerDto> UpdateCustomerProfileAsync(int id, UpdateCustomerInformationDto dto);
        Task DeleteCustomerAsync(int id);
    }
}
