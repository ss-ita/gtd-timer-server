using System;

using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;


namespace Timer.DAL.Timer.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUserManager<User,int> UserManager { get; set; }

        IRepository<Role> Roles { get; set; }
        IRepository<Tasks> Tasks { get; set; }
        IRepository<Preset> Presets { get; set; }
        IRepository<UserRole> UserRoles { get; set; }
        IRepository<Timer.DAL.Entities.Timer> Timers { get; set; }

        void Save();
    }
}