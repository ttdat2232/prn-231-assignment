using Application.Exceptions.Base;
using Microsoft.AspNetCore.Http;
using System;

namespace FUCarRentingSystem.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandler> logger;
        private readonly IHostEnvironment env;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            if(exception is BaseException)
            {
                BaseException baseException = (BaseException) exception;
                context.Response.StatusCode = baseException.StatusCode;
                return context.Response.WriteAsJsonAsync(baseException.Message);
            }
            else
            {
                if (env.IsDevelopment())
                {
                    logger.LogError(exception.Message, exception);
                    return context.Response.WriteAsJsonAsync(exception.Message);
                }
                return context.Response.WriteAsJsonAsync("Sever error");
            }
        }
    }
}
