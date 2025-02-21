using System.Net;
using Application.Contracts;
using Application.ErrorModel;
using Application.Validation.Exceptions.Base;
using Microsoft.AspNetCore.Diagnostics;

namespace Presentation.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        UnauthorizedException => StatusCodes.Status401Unauthorized,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    logger.LogError($"Something went wrong: {contextFeature.Error}");
                    
                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message
                    }.ToString());
                }
            });
        });
    }
}