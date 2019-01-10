using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Timer.DAL.Timer.DAL.Entities;

namespace Timer.DAL.Timer.DAL.Repositories
{
    public class UserRepository :IUserStoreRepository
    { 
        public TimerContext TimerContext { get; set; }

        public IQueryable<User> Users { get; }

        public UserRepository(TimerContext context)
        {
            TimerContext = context;
        }

        public async Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            TimerContext.Users.Add(user);
            await SaveChanges();
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            TimerContext.Users.Attach(user);
            TimerContext.Entry(user).State = EntityState.Modified;
            await SaveChanges();
        }

        public async Task DeleteAsync(User user)
        {
            if (TimerContext.Entry(user).State == EntityState.Detached)
            {
                TimerContext.Users.Attach(user);
            }

           TimerContext.Users.Remove(user);
            await TimerContext.SaveChangesAsync();
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            return await TimerContext.Users.FindAsync(userId);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            User userRes = await Task.Run(() =>
            {
                var user = TimerContext.Users.Where(userToFind => userToFind.UserName == userName).ToList<User>();
                if (user.Count == 0)
                    return null;
                else return user[0];
            });
            return userRes;
        }

        public async Task SetEmailAsync(User user, string email)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            User userToUpdate = await TimerContext.Users.FindAsync(user);
            userToUpdate.Email = email;
        }

        public async Task<string> GetEmailAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            User userToReturnEmail = await TimerContext.Users.FindAsync(user.Id);
            return userToReturnEmail.Email;
        }

        public async Task<bool> GetEmailConfirmedAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            User userToReturnEmailConfirmed = await TimerContext.Users.FindAsync(user.Id);
            return userToReturnEmailConfirmed.EmailConfirmed;
        }

        public async Task SetEmailConfirmedAsync(User user, bool bl)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            User userToConfrim = await TimerContext.Users.FindAsync(user.Id);
            user.EmailConfirmed = bl;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            List<User> list = TimerContext.Users.Where(userToFind => userToFind.Email == email).ToList<User>();
            if (list.Count == 0)
            {
                return null;
            }
            return list[0];
        }

        private async Task SaveChanges()
        {
            await TimerContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (TimerContext != null)
                TimerContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<User> GetAllEntities()
        {
            return TimerContext.Users;
        }
        public IEnumerable<User> GetAllEntitiesByFilter(Func<User,bool> filter)
        {
            return TimerContext.Users.Where(filter);
        }
        public User GetByID(object id)
        {
            return TimerContext.Users.Find(id);
        }
        public void Create(User entity)
        {
            TimerContext.Users.Add(entity);
        }
        public void Delete(object id)
        {
            User entity = TimerContext.Users.Find(id);
            if (entity != null)
                TimerContext.Users.Remove(entity);
        }

        public void Delete(User entityToDelete)
        {
            if (TimerContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                TimerContext.Users.Attach(entityToDelete);
            }
            TimerContext.Users.Remove(entityToDelete);
        }
        public void Update(User entityToUpdate)
        {
            TimerContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public void Save()
        {
            TimerContext.SaveChanges();
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            List<string> roles = (from userRole in TimerContext.UserRoles
                                  join role in TimerContext.Roles
                                on userRole.RoleId equals role.Id
                                  where userRole.UserId == user.Id
                                  select role.Name).ToList<string>();
            return await Task.FromResult((IList<string>)roles);
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            var roleToAdd = (from role in TimerContext.Roles
                             where role.Name == roleName
                             select role).First();
            UserRole userRole = new UserRole
            {
                RoleId = roleToAdd.Id,
                UserId = user.Id,
                User = user,
                Role = roleToAdd
            };
            TimerContext.UserRoles.Add(userRole);
            return TimerContext.SaveChangesAsync();
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            var roleToRemove = (from role in TimerContext.Roles
                                where role.Name == roleName
                                select role).First();
            UserRole userRole = new UserRole
            {
                RoleId = roleToRemove.Id,
                UserId = user.Id,
                User = user,
                Role = roleToRemove
            };
            TimerContext.UserRoles.Remove(userRole);
            return TimerContext.SaveChangesAsync();
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            var check = (from role in TimerContext.Roles
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
                throw new ArgumentNullException("user");
            }

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult<bool>(!String.IsNullOrEmpty(user.PasswordHash));
        }
    }
}
