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
        }

        /// <summary>
        /// Gets or sets timer context
        /// </summary>
        public TimerContext TimerContext { get; set; }

        public async Task<IList<string>> GetAllEmails()
        {
            List<string> emails = (from user in TimerContext.Users
                                   select user.Email).ToList<string>();
            return await Task.FromResult((IList<string>)emails);
        }
    }
}
