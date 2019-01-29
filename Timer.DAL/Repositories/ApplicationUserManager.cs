//-----------------------------------------------------------------------
// <copyright file="ApplicationUserManager.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

using GtdTimerDAL.Entities;
using GtdTimerDAL.TokensProvider;

namespace GtdTimerDAL.Repositories
{
    /// <summary>
    /// Application user manager class
    /// </summary>
    public class ApplicationUserManager : UserManager<User, int>, IApplicationUserManager<User, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserManager" /> class.
        /// </summary>
        /// <param name="store"> I user store instance</param>
        /// <param name="context"> timer context instance </param>
        public ApplicationUserManager(IUserStore<User, int> store, TimerContext context) : base(store)
        {
            this.TimerContext = context;
            this.UserTokenProvider = new UserTokenProvider<IUser>();
        }

        /// <summary>
        /// Gets or sets timer context
        /// </summary>
        public TimerContext TimerContext { get; set; }

        public async Task<IList<string>> GetAllEmails(string roleName)
        {
            Role role = (from rol in TimerContext.Roles
                         where rol.Name == roleName
                         select rol).FirstOrDefault();
            IList<string> emails = new List<string>();

            if (role.Name == GtdCommon.Constant.Constants.UserRole)
            {
                emails = (from user in TimerContext.Users
                          join userRole in TimerContext.UserRoles
                          on user.Id equals userRole.UserId
                          where userRole.RoleId == role.Id
                          select user.Email).ToList<string>().Except(
                         (from user in TimerContext.Users
                          join userRole in TimerContext.UserRoles
                         on user.Id equals userRole.UserId
                          where userRole.RoleId == 2 || userRole.RoleId == 3
                          select user.Email).ToList<string>()).ToList();
            }

            if (role.Name == GtdCommon.Constant.Constants.AdminRole)
            {
                emails = (from user in TimerContext.Users
                          join userRole in TimerContext.UserRoles
                          on user.Id equals userRole.UserId
                          where userRole.RoleId == role.Id
                          select user.Email).ToList<string>();
            }

            return await Task.FromResult((IList<string>)emails);
        }
    }
}
