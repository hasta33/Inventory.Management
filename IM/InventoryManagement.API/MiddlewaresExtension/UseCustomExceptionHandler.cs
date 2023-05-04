using InventoryManagement.API.Services;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Services.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace InventoryManagement.API.MiddlewaresExtension
{
    public static class UseCustomExceptionHandler
    {
        //private readonly ILogger<GenericHelper> _logger2;
        //private GenericHelper genericHelper;

        public static void UseCustomException(this IApplicationBuilder app, ILogger<GenericHelper> logger2)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };

                    context.Response.StatusCode = statusCode;
                    var response = CustomResponseDto<NoContent>.Fail(statusCode, exceptionFeature.Error.Message);

                    logger2.LogError($"Something went wrong: {exceptionFeature.Error}");  //global logging
                    //Console.WriteLine(exceptionFeature.Error);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
