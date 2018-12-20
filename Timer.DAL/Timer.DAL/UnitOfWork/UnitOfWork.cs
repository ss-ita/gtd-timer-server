using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;

using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;

namespace Timer.DAL.Timer.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private TimerContext context;
        private Lazy<IApplicationUserManager<User, int>> userManager;
        private Lazy<IRepository<Role>> roles;
        private bool disposed;

        public IApplicationUserManager<User, int> UserManager
        {
            get => userManager.Value;
            set
            {
                userManager = new Lazy<IApplicationUserManager<User, int>>(() => value);
            }
        }
        public IRepository<Role> Roles
        {
            get => roles.Value;
            set
            {
                roles = new Lazy<IRepository<Role>>(() => value);
            }
        }

        public UnitOfWork(TimerContext context, IApplicationUserManager<User,int> applicationUserManager,IRepository<Role> role)
        {
            this.context = context;
            this.disposed = false;
            this.UserManager = applicationUserManager;
            this.Roles = role;
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