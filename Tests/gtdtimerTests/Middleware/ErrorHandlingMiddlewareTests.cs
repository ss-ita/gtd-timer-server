using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using Common.Exceptions;
using gtdtimer.Middleware;

namespace ErrorHandlingMiddlewareTests
{
    [TestFixture]
    public class ErrorHandlingMiddlewareTests
    {
        [Test]
        public async Task Invoke_GetStatusCode_HtttpStatusCodeNoContent()
        {
            var middleware = new ErrorHandlingMiddleware(next: async (innerhttpcontext) =>
            {
                await Task.Run(() => {
                    throw new UserNotFoundException();
                });
            });
            var context = new DefaultHttpContext();
            await middleware.Invoke(context);
            int actualStatusCode = context.Response.StatusCode;
            int expectedStatusCode = (int)HttpStatusCode.NoContent;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
        [Test]
        public async Task Invoke_GetStatusCode_HtttpStatusCodeInternalServerError()
        {
            var middleware = new ErrorHandlingMiddleware(next: async (innerhttpcontext) =>
            {
                await Task.Run(() => {
                    throw new Exception();
                });
            });
            var context = new DefaultHttpContext();
            await middleware.Invoke(context);
            int actualStatusCode = context.Response.StatusCode;
            int expectedStatusCode = (int)HttpStatusCode.InternalServerError;
            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
    }
}