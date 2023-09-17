using Application.Dtos;
using Application.Dtos.Updates;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        public async Task DeleteCustomerAsync(int id)
        {
            var entityToDelete = await unitOfWork.Customers.GetByIdAsync(new object[] { id });
            if(entityToDelete.RentingTransactions.Count > 0)
            {
                entityToDelete.CustomerStatus = 0;
                unitOfWork.Customers.Update(entityToDelete);
            }
            else
                unitOfWork.Customers.Delete(entityToDelete);
            await unitOfWork.CompleteAsync();
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            return CustomerMapper.ToDto(await unitOfWork.Customers.GetByIdAsync(new object[] {id}));
        }

        public async Task<CustomerDto> UpdateCustomerProfileAsync(int id, UpdateCustomerInformationDto dto)
        {
            var existed = await unitOfWork.Customers.GetByIdAsync(new object[] {id});
            CustomerMapper.ToEntity(dto, ref existed);
            existed = unitOfWork.Customers.Update(existed);
            await unitOfWork.CompleteAsync();
            return CustomerMapper.ToDto(existed);
        }
    }
}
