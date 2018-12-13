using gtdtimer.Timer.DAL.Entities;
using gtdtimer.Timer.DAL.Repositories;

namespace gtdtimer.Timer.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; set; }
        IRepository<Role> Roles { get; set; }
        //IRepository<UserRole> UserRoles { get; set; }

        void Save();
    }
}