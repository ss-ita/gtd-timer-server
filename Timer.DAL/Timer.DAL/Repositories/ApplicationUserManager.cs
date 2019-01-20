using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Timer.DAL.Timer.DAL.Entities;

namespace Timer.DAL.Timer.DAL.Repositories
{
    public class ApplicationUserManager: UserManager<User, int>, IApplicationUserManager<User,int>
    {
        TimerContext timerContext { get; set; }

        public ApplicationUserManager(IUserStore<User, int> store, TimerContext context) : base(store)
        {
            timerContext = context;
        }

        public async Task<IList<string>> GetAllEmails()
        {
            List<string> emails = (from user in timerContext.Users
                                   select user.Email).ToList<string>();
            return await Task.FromResult((IList<string>)emails);
        }
    }
}
