using Common.Constant;
using Common.IoC;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Timer.DAL.Timer.DAL.Entities;

namespace ServiceTier.Services
{
    public class JWTManager
    {
        public virtual string GenerateToken(User user, IList<string> userRoles)
        {
            var claims = new List<Claim>
            {
                new Claim(Constants.ClaimUserId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IoCContainer.Configuration["JWTSecretKey"]));

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(Constants.TokenExpirationInHours),
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
