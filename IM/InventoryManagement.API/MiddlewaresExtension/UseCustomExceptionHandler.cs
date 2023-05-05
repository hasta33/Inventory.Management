using InventoryManagement.Core.DTOs;
using InventoryManagement.Services.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace InventoryManagement.API.MiddlewaresExtension
{
    public static class UseCustomExceptionHandler
    {
        private static ILogger _logger;
        public static void UseCustomException(this IApplicationBuilder app)
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

                    //Bu loglama tekrar değerlendirilecek
                    if (statusCode == 500)
                    {
                        _logger.LogError($"{exceptionFeature.Error}");
                    } else if (statusCode == 400)
                    {
                        _logger.LogWarning($"{exceptionFeature.Error}");
                    } else if (statusCode == 404)
                    {
                        _logger.LogInformation($"{exceptionFeature.Error}");
                    } else
                    {
                        _logger.LogInformation($"{exceptionFeature.Error}");
                    }

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }

    }
}
