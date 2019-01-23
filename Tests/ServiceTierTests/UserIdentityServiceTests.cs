//-----------------------------------------------------------------------
// <copyright file="UserIdentityServiceTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

using GtdCommon.Constant;
using GtdServiceTier.Services;

namespace GtdServiceTierTests
{
    [TestFixture]
    public class UserIdentityServiceTests
    {
        private Mock<IHttpContextAccessor> httpContextAccessor;

        private UserIdentityService subject;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            subject = new UserIdentityService(httpContextAccessor.Object);
        }

        /// <summary>
        /// Get User Id test
        /// </summary>
        [Test]
        public void GetUserId()
        {
            int userId = 1;
            var claim = new ClaimsIdentity();
            claim.AddClaim(new Claim(Constants.ClaimUserId, userId.ToString()));
            var httpContext = new Mock<HttpContext>();
            var user = new Mock<ClaimsPrincipal>();

            httpContextAccessor.Setup(_ => _.HttpContext).Returns(httpContext.Object);
            httpContext.Setup(_ => _.User).Returns(user.Object);
            user.Setup(_ => _.Identity).Returns(claim);

            var actual = subject.GetUserId();

            Assert.AreEqual(actual, userId);
        }
    }
}
