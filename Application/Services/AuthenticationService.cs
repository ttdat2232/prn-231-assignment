using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Models.Configuration;
using System.Linq.Expressions;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationSerivce
    {
        private readonly AppConfiguration appConfig;
        private readonly IUnitOfWork unitOfWork;

        public AuthenticationService(AppConfiguration appConfig, IUnitOfWork unitOfWork)
        {
            this.appConfig = appConfig;
            this.unitOfWork = unitOfWork;
        }

        public async Task<AuthenticateResponse> LoginAsync(AuthenticateRequest request)
        {
            if (appConfig.Username != null && appConfig.Password != null && appConfig.Username.ToLower().Equals(request.Email.ToLower()) && appConfig.Password.Equals(request.Password))
            {
                return new AuthenticateResponse() { CustomerId = -1, IsAdmin = true};
            }
            var find = await unitOfWork.Customers.GetAsync(expression: c => c.Email.ToLower().Equals(request.Email.ToLower()) && c.Password != null && c.Password.Equals(request.Password) && c.CustomerStatus == 1);
            if(find.Values.Count > 0)
            {
                return new AuthenticateResponse() { CustomerId = find.Values.First().CustomerId };
            }
            throw new UnauthorizeExpcetion("Wrong email or password");

        }

        public async Task<AuthenticateResponse> RegisterAsync(AuthenticateRequest request)
        {
            await unitOfWork.Customers.GetAsync(expression: c => c.Email.ToLower().Equals(request.Email.ToLower()) && c.CustomerStatus == 1)
                .ContinueWith(t =>
                {
                    if (t.Result.Values.Count > 0)
                        throw new ConflictExpcetion("Email is already existed");
                });
            var entityToAdd = await unitOfWork.Customers.AddAsync(new Customer()
            {
                Email = request.Email,
                Password = request.Password,
                CustomerStatus = 1,
            });
            await unitOfWork.CompleteAsync();
            return new AuthenticateResponse()
            {
                CustomerId = entityToAdd.CustomerId,
            };
        }
    }
}
