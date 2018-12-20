using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Timer.DAL.Timer.DAL.Entities;

namespace Timer.DAL.Timer.DAL.Repositories
{
    public class ApplicationUserManager : UserManager<User, int>, IApplicationUserManager<User,int>
    {
        public ApplicationUserManager(IUserStore<User, int> store) : base(store)
        {
           
        }
    }
}
