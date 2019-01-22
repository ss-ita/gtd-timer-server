using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Common.Exceptions;


namespace gtdtimer.Middleware

{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
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

        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var exceptionCode = GetExceptionCode(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exceptionCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = exception.Message,
                StatusCode = exceptionCode
            }));
        }


        private HttpStatusCode GetExceptionCode(Exception exception)
        {
            HttpStatusCode exceptionCode;
            this.logger.LogError($"Error occured in {exception.Source} {exception.TargetSite}, with message: {exception.Message}");
            switch (exception)
            {
                case UserNotFoundException _:
                    exceptionCode = HttpStatusCode.NoContent;
                    break;
                case PresetNotFoundException _:
                case TaskNotFoundException _:
                    exceptionCode = HttpStatusCode.NotFound;
                    break;
                case UserAlreadyExistsException _:
                    exceptionCode = HttpStatusCode.Conflict;
                    break;
                case PasswordMismatchException _:
                case LoginFailedException _:
                case AccessDeniedException _:
                    exceptionCode = HttpStatusCode.BadRequest;
                    break;
                default:
                    exceptionCode = HttpStatusCode.InternalServerError;
                    break;
            }
            return exceptionCode;
        }
    }
}
