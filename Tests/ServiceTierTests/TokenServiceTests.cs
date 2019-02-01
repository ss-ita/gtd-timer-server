//-----------------------------------------------------------------------
// <copyright file="TokenServiceTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

using GtdServiceTier.Services;
using GtdTimerDAL.Entities;
using GtdTimerDAL.Repositories;
using GtdTimerDAL.UnitOfWork;

namespace GtdServiceTierTests
{
    [TestFixture]
    class TokenServiceTests
    {
        private const string userEmail = "User email";
        private const int tokenId = 4;
        private readonly List<Token> tokens = new List<Token>() { new Token() { Id = tokenId } };
        private TokenService subject;
        private Mock<IUnitOfWork> unitOfWork;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new TokenService(unitOfWork.Object);
        }

        /// <summary>
        /// Create token test
        /// </summary>
        [Test]
        public void Create()
        {
            Token token = new Token();
            var tokenRepository = new Mock<IRepository<Token>>();

            unitOfWork.Setup(_ => _.Tokens).Returns(tokenRepository.Object);
            subject.CreateToken(token);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Get token by user email test
        /// </summary>
        [Test]
        public void GetTokenByUserEmail()
        {
            Token token = new Token() { Id = tokenId };
            var tokenRepository = new Mock<IRepository<Token>>();

            unitOfWork.Setup(_ => _.Tokens).Returns(tokenRepository.Object);
            unitOfWork.Setup(_ => _.Tokens.GetAllEntitiesByFilter(It.IsAny<Func<Token, bool>>())).Returns(tokens);

            Assert.AreEqual(subject.GetTokenByUserEmail(userEmail).Id, token.Id);
        }
    }
}
