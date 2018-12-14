using System;
using System.Security.Claims;
using System.Text;

using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;
using Common.Model;
using Common.Constant;
using ServiceTier.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Common.Exceptions;

namespace gtdtimer.Services
{
    public class LogInService : ILogInService
    {
        private IUnitOfWork userManager;

        public LogInService(IUnitOfWork userManager)
        {
            this.userManager = userManager;
        }

        public string CreateToken(LoginModel model)
        {
            var user = userManager.UserManager.FindByEmailAsync(model.Email).Result;

            if (user == null || !(user.PasswordHash == model.Password))
            {
                throw new UserNotFoundException();
            }

            var token = GeneretaToken(user);

            return token;
        }

        private static string GeneretaToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.SecretKey));

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                issuer: "Tokens:Issuer",
                audience: "Tokens:Audience"
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

    }
}
