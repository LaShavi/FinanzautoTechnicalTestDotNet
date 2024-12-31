using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Net;

namespace TechnicalTestDotNet.API.Middleware.ExceptionManager
{
    public class ExceptionManager
    {
        //public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        //{
        //    app.UseExceptionHandler(appError =>
        //    {
        //        appError.Run(async context =>
        //        {
        //            Logger log = LogManager.GetCurrentClassLogger();

        //            context.Response.ContentType = "application/json";
        //            var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        //            CustomHttpException customHttpException = new();
        //            if (contextFeature.Error is CustomHttpException)
        //            {
        //                customHttpException = contextFeature.Error as CustomHttpException;

        //                await context.Response.WriteAsync(new ErrorDetails()
        //                {
        //                    StatusCode = customHttpException.StatusCode,
        //                    Messages = customHttpException.Messages,
        //                    MessageUser = customHttpException.MessageUser,
        //                    PatchError = context.Request.Path
        //                }.ToString());


        //            }
        //            else
        //            {
        //                if (contextFeature != null)
        //                {
        //                    log.Warn($"ERROR no controlado en la aplicación =>  [{context.Request.Path}]");
        //                    log.Error($"{contextFeature.Error.Message} ||=>  {contextFeature.Error.InnerException?.Message}");
        //                    await context.Response.WriteAsync(new ErrorDetails()
        //                    {
        //                        StatusCode = HttpStatusCode.InternalServerError,
        //                        Messages = new List<string> { "Internal Server Error.", contextFeature.Error.Message, contextFeature.Error.InnerException?.Message },
        //                        MessageUser = "Ha ocurrido un error, por favor inténtelo de nuevo más tarde.",
        //                        PatchError = context.Request.Path
        //                    }.ToString());
        //                }
        //            }
        //        });
        //    });
        //}
    }
}
