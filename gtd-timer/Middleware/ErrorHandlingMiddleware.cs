using Microsoft.AspNetCore.Http;
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

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
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
            var result = JsonConvert.SerializeObject(new
            {
                error = exception.Message,
                code = exceptionCode
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exceptionCode;
            return context.Response.WriteAsync(result);
        }

        private HttpStatusCode GetExceptionCode(Exception exception)
        {
            HttpStatusCode exceptionCode;
            switch (exception)
            {
                case UserNotFoundException _:
                    exceptionCode = HttpStatusCode.NoContent;
                    break;
                case UserAlreadyExistsException _:
                    exceptionCode = HttpStatusCode.Conflict;
                    break;
                case IncorrectPasswordException _:
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
