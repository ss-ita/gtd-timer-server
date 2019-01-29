using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using GtdTimerDAL.Entities;
using System.Security.Cryptography;

namespace GtdTimerDAL.TokensProvider
{
    public class UserTokenProvider<TUser> : IUserTokenProvider<User, int> where TUser : class, IUser
    {
        public Task<string> GenerateAsync(string purpose, UserManager<User, int> manager, User user)
        {
            Guid guid = Guid.NewGuid();
            string emailToken = this.Hash(guid.ToString());
            emailToken = emailToken.Replace("+", "WlM3x").Replace("/","He3RZsw");
            emailToken = emailToken.Remove(emailToken.Length - 3);
            user.SecurityStamp = emailToken.ToString();
            manager.UpdateAsync(user).GetAwaiter();
            return Task.FromResult<string>(emailToken.ToString());
        }

        public Task<bool> IsValidProviderForUserAsync(UserManager<User, int> manager, User user)
        {
            if (manager == null) throw new ArgumentNullException();
            else
            {
                return Task.FromResult<bool>(manager.SupportsUserPassword);
            }
        }

        public Task NotifyAsync(string token, UserManager<User, int> manager, User user)
        {
            return Task.FromResult<int>(0);
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<User, int> manager, User user)
        {
            return Task.FromResult<bool>(user.SecurityStamp.ToString() == token);
        }

        public string Hash(string key)
        {
            byte[] salt;
            byte[] buffer2;
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(key, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
