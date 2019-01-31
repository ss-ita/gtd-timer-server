//-----------------------------------------------------------------------
// <copyright file="UserTokenProvider.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity;

using GtdTimerDAL.Entities;

namespace GtdTimerDAL.TokensProvider
{
    /// <summary>
    /// Class to generate user tokens
    /// </summary>
    public class UserTokenProvider<TUser> : IUserTokenProvider<User, int> where TUser : class, IUser
    {
        /// <summary>
        /// Generate a token for a user with a specific purpose
        /// </summary>
        /// <param name="purpose">purpose of generating token</param>
        /// <param name="manager">instance of user manager</param>
        /// <param name="user">user</param>
        /// <returns>token</returns>
        public Task<string> GenerateAsync(string purpose, UserManager<User, int> manager, User user)
        {
            Guid guid = Guid.NewGuid();
            string emailToken = this.Hash(guid.ToString());
            emailToken = emailToken.Replace("+", "WlM3x").Replace("/","He3RZsw");
            emailToken = emailToken.Remove(emailToken.Length - 3);
            return Task.FromResult<string>(emailToken.ToString());
        }

        /// <summary>
        /// Returns true if provider can be used for this user, i.e. could require a user to have an email
        /// </summary>
        /// <param name="manager">instance of user manager</param>
        /// <param name="user">user</param>
        /// <returns>result of provider validation</returns>
        public Task<bool> IsValidProviderForUserAsync(UserManager<User, int> manager, User user)
        {
            if (manager == null) throw new ArgumentNullException();
            else
            {
                return Task.FromResult<bool>(manager.SupportsUserPassword);
            }
        }

        /// <summary>
        /// Notifies the user that a token has been generated, for example an email or sms could be sent, or 
        /// this can be a no-op
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="manager">instance of user manager</param>
        /// <param name="user">user</param>
        /// <returns>result of notifying</returns>
        public Task NotifyAsync(string token, UserManager<User, int> manager, User user)
        {
            return Task.FromResult<int>(0);
        }

        /// <summary>
        /// Validate a token for a user with a specific purpose
        /// </summary>
        /// <param name="purpose">purpose of generating token</param>
        /// <param name="token">token</param>
        /// <param name="manager">instance of user manager</param>
        /// <param name="user">user</param>
        /// <returns>result of validation</returns>
        public Task<bool> ValidateAsync(string purpose, string token, UserManager<User, int> manager, User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Function for hashing a string  
        /// </summary>
        /// <param name="key">secret key</param>
        /// <returns></returns>
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
