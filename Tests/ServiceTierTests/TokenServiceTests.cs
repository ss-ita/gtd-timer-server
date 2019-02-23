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
using GtdCommon.Constant;

namespace GtdServiceTierTests
{
    [TestFixture]
    public class TokenServiceTests
    {
        private const string UserEmail = "User email";
        private const int TokenId = 4;
        private readonly List<Token> tokens = new List<Token>() { new Token() { Id = TokenId } };
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
        /// Delete token test
        /// </summary>
        [Test]
        public void DeleteTokenByUserEmail()
        {
            Token token = new Token();
            var tokenRepository = new Mock<IRepository<Token>>();

            unitOfWork.Setup(_ => _.Tokens).Returns(tokenRepository.Object);
            unitOfWork.Setup(_ => _.Tokens.GetAllEntitiesByFilter(It.IsAny<Func<Token, bool>>())).Returns(tokens);
            subject.DeleteTokenByUserEmail(UserEmail, TokenType.EmailVerification);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Get token by user email test
        /// </summary>
        [Test]
        public void GetTokenByUserEmail()
        {
            Token token = new Token() { Id = TokenId };
            var tokenRepository = new Mock<IRepository<Token>>();

            unitOfWork.Setup(_ => _.Tokens).Returns(tokenRepository.Object);
            unitOfWork.Setup(_ => _.Tokens.GetAllEntitiesByFilter(It.IsAny<Func<Token, bool>>())).Returns(tokens);

            Assert.AreEqual(subject.GetTokenByUserEmail(UserEmail, TokenType.EmailVerification).Id, token.Id);
        }
    }
}
