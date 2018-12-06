using gtdtimer.Timer.DAL.Entities;
using gtdtimer.Timer.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gtdtimer.Timer.DAL.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        public IRepository<User> Users { get; set; }
        public IRepository<Role> Roles { get; set; }

        private TimerContext context;

        public UnitOfWork(TimerContext context)
        {
            this.context = context;

            this.Users = new Repository<User>(context);
            this.Roles = new Repository<Role>(context);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
