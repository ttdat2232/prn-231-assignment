using Domain.Models.Configuration;
using FUCarRentingSystem;
using FUCarRentingSystem.Middlewares;
using Repositories;
var builder = WebApplication.CreateBuilder(args);

var appConfig = builder.Configuration.Get<AppConfiguration>();
if (appConfig != null )
{
    // TODO: move those stuff such as: secret key, connection string to something like azure's enviroment, aws ...
    appConfig.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    appConfig.Username = builder.Configuration["Admin:Username"];
    appConfig.Password = builder.Configuration["Admin:Password"];
    builder.Services.AddDependencyInjection(appConfig.ConnectionString);
    builder.Services.AddApiServices();
    builder.Services.AddSingleton(appConfig);
}
var allowConnectingApplication = "AngularApplication";
builder.Services.AddCors(opts =>
{
    opts.AddPolicy(
        name: allowConnectingApplication, 
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowCredentials()
                .SetIsOriginAllowed(host => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .Build();
        });
});
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowConnectingApplication);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
