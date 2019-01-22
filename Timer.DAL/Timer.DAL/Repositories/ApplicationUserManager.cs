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

        public async Task<IList<string>> GetAllEmails(string roleName)
        {
            Role role = (from rol in timerContext.Roles
                         where rol.Name == roleName
                         select rol).FirstOrDefault();
            IList<string> emails = new List<string>();

            if (role.Name == Common.Constant.Constants.UserRole)
            {
                emails = (from user in timerContext.Users
                          join userRole in timerContext.UserRoles
                          on user.Id equals userRole.UserId
                          where userRole.RoleId == role.Id
                          select user.Email).ToList<string>().Except(
                         (from user in timerContext.Users
                          join userRole in timerContext.UserRoles
                         on user.Id equals userRole.UserId
                          where userRole.RoleId == 2 || userRole.RoleId == 3
                          select user.Email).ToList<string>()).ToList();
            }

            if (role.Name == Common.Constant.Constants.AdminRole)
            {
                emails = (from user in timerContext.Users
                          join userRole in timerContext.UserRoles
                          on user.Id equals userRole.UserId
                          where userRole.RoleId == role.Id
                          select user.Email).ToList<string>().Except(
                         (from user in timerContext.Users
                          join userRole in timerContext.UserRoles
                         on user.Id equals userRole.UserId
                          where userRole.RoleId == 3
                          select user.Email).ToList<string>()).ToList();
            }

            if (role.Name == Common.Constant.Constants.SuperAdminRole)
            {
                emails = (from user in timerContext.Users
                          join userRole in timerContext.UserRoles
                          on user.Id equals userRole.UserId
                          where userRole.RoleId == role.Id
                          select user.Email).ToList<string>();
            }

            return await Task.FromResult((IList<string>)emails);
        }
    }
}
