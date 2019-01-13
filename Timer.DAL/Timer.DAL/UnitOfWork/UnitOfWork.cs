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
        private Lazy<IRepository<Tasks>> tasks;
        private Lazy<IRepository<UserRole>> userRoles;
        private Lazy<IRepository<Preset>> presets;
        private Lazy<IRepository<Timer.DAL.Entities.Timer>> timers;

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

        public IRepository<Tasks> Tasks
        {
            get => tasks.Value;
            set
            {
                tasks = new Lazy<IRepository<Tasks>>(() => value);
            }
        }

        public IRepository<Preset> Presets
        {
            get => presets.Value;
            set
            {
                presets = new Lazy<IRepository<Preset>>(() => value);
            }
        }

        public IRepository<Timer.DAL.Entities.Timer> Timers
        {
            get => timers.Value;
            set
            {
                timers = new Lazy<IRepository<Timer.DAL.Entities.Timer>>(() => value);
            }
        }

        public IRepository<UserRole> UserRoles
        {
            get => userRoles.Value;
            set
            {
                userRoles = new Lazy<IRepository<UserRole>>(() => value);
            }
        }

        public UnitOfWork(TimerContext context, IApplicationUserManager<User, int> applicationUserManager, IRepository<Role> role, IRepository<Preset> preset, IRepository<Timer.DAL.Entities.Timer> timer, IRepository<Tasks> tasks, IRepository<UserRole> userRole)
        {
            this.context = context;
            this.disposed = false;
            this.UserManager = applicationUserManager;
            this.Roles = role;
            this.Tasks = tasks;
            this.Presets = preset;
            this.Timers = timer;
            this.UserRoles = userRole;
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