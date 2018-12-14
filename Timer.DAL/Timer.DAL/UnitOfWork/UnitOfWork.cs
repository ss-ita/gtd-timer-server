using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;

namespace Timer.DAL.Timer.DAL.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private UserManager<User,int> userManager;
        public UserManager<User,int> UserManager
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
                if(Roles == null)
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

        public UnitOfWork(TimerContext context)
        {
            this.context = context;
        }
        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
