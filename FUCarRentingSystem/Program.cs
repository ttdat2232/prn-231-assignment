using Domain.Models.Configuration;
using FUCarRentingSystem;
using FUCarRentingSystem.Middlewares;
using Repositories;
var builder = WebApplication.CreateBuilder(args);

var appConfig = builder.Configuration.Get<AppConfiguration>();
if (appConfig != null )
{
    appConfig.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    appConfig.Username = builder.Configuration["Admin:Username"];
    appConfig.Password = builder.Configuration["Admin:Password"];
    builder.Services.AddDependencyInjection(appConfig.ConnectionString);
    builder.Services.AddApiServices();
    builder.Services.AddSingleton(appConfig);
}
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
