﻿using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;

namespace NLayer.API.Middlewares;

public static class UseCustomExceptionHandler
{
    public static void UseCustomException(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            //run sonlandırıcı bir görev üstlenir bu bölüme gelindiğinde controller a geçilemez.
            config.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                var statusCode = exceptionFeature.Error switch
                {
                    ClientSideException => 400,
                    NotFoundException =>404,
                    _ => 500
                };

                context.Response.StatusCode = statusCode;
                var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));

            });
        });
    }
}