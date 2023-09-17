using Application.Interfaces;
using Application.Services;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Data;

namespace Repositories
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, string? connectionString) 
        {
            if(connectionString != null)
                services.AddDbContext<FUCarRentingManagementContext>(opts => opts.UseSqlServer(connectionString));
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IAuthenticationSerivce, AuthenticationService>();
            return services;
        }
    }
}
