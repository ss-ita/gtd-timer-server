//-----------------------------------------------------------------------
// <copyright file="ErrorHandlingMiddlewareTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

using GtdCommon.Exceptions;
using GtdTimer.Middleware;

namespace GtdTimerTests.Middleware
{
    [TestFixture]
    public class ErrorHandlingMiddlewareTests
    {
        /// <summary>
        /// Invoke Gets Status Code No Content test
        /// </summary>
        /// <returns>result of testing exception</returns>
        [Test]
        public async Task Invoke_GetStatusCode_HtttpStatusCodeNoContent()
        {
            var log = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(
                next: async (innerhttpcontext) =>
            { 
                await Task.Run(() =>
                {
                    throw new UserNotFoundException();
                });
            }, 
                logger: log.Object);
            var context = new DefaultHttpContext();
            await middleware.Invoke(context);
            int actualStatusCode = context.Response.StatusCode;
            int expectedStatusCode = (int)HttpStatusCode.NoContent;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }

        /// <summary>
        /// Invoke Gets Status Code Internal Server Error
        /// </summary>
        /// <returns>result of testing exception</returns>
        [Test]
        public async Task Invoke_GetStatusCode_HtttpStatusCodeInternalServerError()
        {
            var log = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(
                next: async (innerhttpcontext) =>
            {
                await Task.Run(() =>
                {
                    throw new Exception();
                });
            },
                logger: log.Object);
            var context = new DefaultHttpContext();
            await middleware.Invoke(context);
            int actualStatusCode = context.Response.StatusCode;
            int expectedStatusCode = (int)HttpStatusCode.InternalServerError;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
    }
}