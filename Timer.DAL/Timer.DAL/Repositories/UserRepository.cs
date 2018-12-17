using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Timer.DAL.Timer.DAL.Entities;

namespace Timer.DAL.Timer.DAL.Repositories
{
    public class UserRepository : IUserStore<User, int>, IUserEmailStore<User, int>, IRepository<User>
    {
        public TimerContext timerContext { get; set; }

        public UserRepository(TimerContext context)
        {
            timerContext = context;
        }

        public async Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            timerContext.Users.Add(user);
            await SaveChanges();
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            timerContext.Users.Attach(user);
            timerContext.Entry(user).State = EntityState.Modified;
            await SaveChanges();
        }

        public async Task DeleteAsync(User user)
        {
            if (timerContext.Entry(user).State == EntityState.Detached)
            {
                timerContext.Users.Attach(user);
            }

            timerContext.Users.Remove(user);
            await timerContext.SaveChangesAsync();
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            return await timerContext.Users.FindAsync(userId);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            User userRes = await Task.Run(() =>
            {
                var user = timerContext.Users.Where(userToFind => userToFind.UserName == userName).ToList<User>();
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

            User userToUpdate = await timerContext.Users.FindAsync(user);
            userToUpdate.Email = email;
        }

        public async Task<string> GetEmailAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            User userToReturnEmail = await timerContext.Users.FindAsync(user.Id);
            return userToReturnEmail.Email;
        }

        public async Task<bool> GetEmailConfirmedAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            User userToReturnEmailConfirmed = await timerContext.Users.FindAsync(user.Id);
            return userToReturnEmailConfirmed.EmailConfirmed;
        }

        public async Task SetEmailConfirmedAsync(User user, bool bl)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            User userToConfrim = await timerContext.Users.FindAsync(user.Id);
            user.EmailConfirmed = bl;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            List<User> list = timerContext.Users.Where(userToFind => userToFind.Email == email).ToList<User>();
            if (list.Count == 0)
            {
                return null;
            }
            return list[0];
        }

        private async Task SaveChanges()
        {
            await timerContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (timerContext != null)
                timerContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<User> GetAll()
        {
            return timerContext.Users;
        }
        public User GetByID(object id)
        {
            return timerContext.Users.Find(id);
        }
        public void Create(User entity)
        {
            timerContext.Users.Add(entity);
        }
        public void Delete(object id)
        {
            User entity = timerContext.Users.Find(id);
            if (entity != null)
                timerContext.Users.Remove(entity);
        }

        public void Delete(User entityToDelete)
        {
            if (timerContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                timerContext.Users.Attach(entityToDelete);
            }
            timerContext.Users.Remove(entityToDelete);
        }
        public void Update(User entityToUpdate)
        {
            timerContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public void Save()
        {
            timerContext.SaveChanges();
        }
    }
}
