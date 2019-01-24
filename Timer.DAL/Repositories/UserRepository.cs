//-----------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using GtdTimerDAL.Entities;

namespace GtdTimerDAL.Repositories
{
    /// <summary>
    /// class which implement user store repository
    /// </summary>
    public class UserRepository : Repository<User>, IUserStoreRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class.
        /// </summary>
        /// <param name="context"> timer context instance </param>
        public UserRepository(TimerContext context) : base(context)
        {
            this.Timercontext = context;
        }

        public async Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Timercontext.Users.Add(user);
            await this.SaveChanges();
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Timercontext.Users.Attach(user);
            Timercontext.Entry(user).State = EntityState.Modified;
            await this.SaveChanges();
        }

        public async Task DeleteAsync(User user)
        {
            if (Timercontext.Entry(user).State == EntityState.Detached)
            {
                Timercontext.Users.Attach(user);
            }

           Timercontext.Users.Remove(user);
            await Timercontext.SaveChangesAsync();
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            return await Timercontext.Users.FindAsync(userId);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            User userRes = await Task.Run(() =>
            {
                var user = Timercontext.Users.Where(userToFind => userToFind.UserName == userName).ToList<User>();
                if (user.Count == 0)
                {
                    return null;
                }
                else
                {
                    return user[0];
                }
            });
            return userRes;
        }

        public async Task SetEmailAsync(User user, string email)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            User userToUpdate = await Timercontext.Users.FindAsync(user);
            userToUpdate.Email = email;
        }

        public async Task<string> GetEmailAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            User userToReturnEmail = await Timercontext.Users.FindAsync(user.Id);
            return userToReturnEmail.Email;
        }

        public async Task<bool> GetEmailConfirmedAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            User userToReturnEmailConfirmed = await Timercontext.Users.FindAsync(user.Id);
            return userToReturnEmailConfirmed.EmailConfirmed;
        }

        public async Task SetEmailConfirmedAsync(User user, bool bl)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            User userToConfrim = await Timercontext.Users.FindAsync(user.Id);
            user.EmailConfirmed = bl;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            List<User> list = Timercontext.Users.Where(userToFind => userToFind.Email == email).ToList<User>();
            if (list.Count == 0)
            {
                return null;
            }

            return list[0];
        }

        public void Dispose()
        {
            if (this.Timercontext != null)
            {
                this.Timercontext.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            List<string> roles = (from userRole in Timercontext.UserRoles
                                  join role in Timercontext.Roles
                                on userRole.RoleId equals role.Id
                                  where userRole.UserId == user.Id
                                  select role.Name).ToList<string>();
            return await Task.FromResult((IList<string>)roles);
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            var roleToAdd = (from role in Timercontext.Roles
                             where role.Name == roleName
                             select role).First();
            UserRole userRole = new UserRole
            {
                RoleId = roleToAdd.Id,
                UserId = user.Id,
                User = user,
                Role = roleToAdd
            };
            Timercontext.UserRoles.Add(userRole);
            return Timercontext.SaveChangesAsync();
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            var roleToRemove = (from role in Timercontext.Roles
                                where role.Name == roleName
                                select role).First();
            UserRole userRole = new UserRole
            {
                RoleId = roleToRemove.Id,
                UserId = user.Id,
                User = user,
                Role = roleToRemove
            };
            Timercontext.UserRoles.Remove(userRole);
            return Timercontext.SaveChangesAsync();
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            var check = (from role in Timercontext.Roles
             where role.Name == roleName
             select role).First();

            if (check != null)
            {
                return false;
            }

            return true;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult<bool>(!string.IsNullOrEmpty(user.PasswordHash));
        }

        private async Task SaveChanges()
        {
            await Timercontext.SaveChangesAsync();
        }
    }
}
