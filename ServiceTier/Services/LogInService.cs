using System;
using System.Security.Claims;
using System.Text;

using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;
using Common.ModelsDTO;
using Common.Constant;
using ServiceTier.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Common.Exceptions;
using Newtonsoft.Json;
using Common.IoC;

namespace gtdtimer.Services
{
    public class LogInService : ILogInService
    {
        private IUnitOfWork userManager;

        public LogInService(IUnitOfWork userManager)
        {
            this.userManager = userManager;
        }

        public string CreateToken(LoginDTO model)
        {
            var user = userManager.UserManager.FindByEmailAsync(model.Email).Result;

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if(!userManager.UserManager.CheckPasswordAsync(user, model.Password).Result)
            {
                throw new IncorectLoginException();
            }

            var token = GenerateToken(user);

            return token;
        }

        private static string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(Constants.ClaimUserId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IoCContainer.Configuration["JWTSecretKey"]));

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(Constants.Validity),
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                issuer: "Tokens:Issuer",
                audience: "Tokens:Audience"
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return JsonConvert.SerializeObject(new { access_token = tokenString });
        }

    }
}
