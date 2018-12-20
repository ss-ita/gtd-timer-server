﻿using System;
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
using Newtonsoft.Json;

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

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if(user.PasswordHash != model.Password)
            {
                throw new IncorrectPasswordException();
            }

            var token = GenerateToken(user);

            return token;
        }

        private static string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.SecretKey));

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