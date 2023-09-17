using System.Text.Json.Serialization;

namespace FUCarRentingSystem
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services) 
        {
            services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            return services;
        }   
    }
}
