using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;

using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;

namespace Timer.DAL.Timer.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationUserManager userManager;
        public ApplicationUserManager UserManager
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

        private IRepository<Role> roles;
        public IRepository<Role> Roles { get; set; }

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