using Microsoft.AspNet.Identity;
using System;

using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;

namespace Timer.DAL.Timer.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private UserManager<User, int> userManager;
        public UserManager<User, int> UserManager
        {
            get
            {
                if (userManager == null)
                {
                    userManager = new ApplicationUserManager(new UserRepository(context));
                }
                return userManager;
            }
            set
            {
                userManager = value;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (Roles == null)
                {
                    Roles = new Repository<Role>(context);
                }
                return Roles;
            }
            set
            {
                Roles = value;
            }
        }

        private TimerContext context;
        private bool disposed;

        public UnitOfWork(TimerContext context)
        {
            this.context = context;
            this.disposed = false;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}