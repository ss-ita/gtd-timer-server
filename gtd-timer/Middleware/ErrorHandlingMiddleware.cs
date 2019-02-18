//-----------------------------------------------------------------------
// <copyright file="ErrorHandlingMiddleware.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using GtdCommon.Exceptions;

namespace GtdTimer.Middleware
{
    /// <summary>
    /// class for handling different types of custom errors
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// instance of request delegate
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// interface for logger
        /// </summary>
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware" /> class.
        /// </summary>
        /// <param name="next">instance of request delegate</param>
        /// <param name="logger">class which gives right for logging</param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        /// <summary>
        /// implementation of chain of responsibility pattern
        /// </summary>
        /// <param name="context">http context</param>
        /// <returns>result of passing action context handler</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Method for handling custom exceptions
        /// </summary>
        /// <param name="context">http context</param>
        /// <param name="exception">custom exception</param>
        /// <returns>result of handling exception</returns>
        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var exceptionCode = this.GetExceptionCode(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exceptionCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                exception.Message,
                StatusCode = exceptionCode
            }));
        }

        /// <summary>
        /// Method for define type of exception
        /// </summary>
        /// <param name="exception">custom exception</param>
        /// <returns>result of defining type of exception</returns>
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
                case InvalidTokenException _:
                case PasswordMismatchException _:
                case LoginFailedException _:
                case AccessDeniedException _:
                case ImportErrorException _:
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
