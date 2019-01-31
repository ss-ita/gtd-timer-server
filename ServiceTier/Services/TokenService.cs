//-----------------------------------------------------------------------
// <copyright file="TokenService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using GtdTimerDAL.Entities;
using GtdTimerDAL.UnitOfWork;
using System.Linq;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// class which implements ITokenService interface
    /// </summary>
    public class TokenService : BaseService, ITokenService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService" /> class.
        /// </summary>
        /// <param name="unitOfWork">instance of unit of work</param>
        public TokenService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public void CreateToken(Token token)
        {
            UnitOfWork.Tokens.Create(token);
            UnitOfWork.Save();
        }

        public Token GetTokenByUserId(int userId)
        {
            var userToken = UnitOfWork.Tokens.GetAllEntitiesByFilter(token => token.UserId == userId)
                .Select(token => token);

            return userToken.FirstOrDefault();
        }
    }
}
