//-----------------------------------------------------------------------
// <copyright file="ApplicationUserManager.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
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
        public ApplicationUserManager(IUserStore<User, int> store, 
            IRepository<Role> roles,
            IRepository<UserRole> userRoles,
            IRepository<User> users) : base(store)
        {
            Roles = roles;
            UserRoles = userRoles;
            Users = users;
            this.UserTokenProvider = new UserTokenProvider<IUser>();
        }

        /// <summary>
        /// Roles table
        /// </summary>
        private Lazy<IRepository<Role>> roles;

        /// <summary>
        /// User roles table
        /// </summary>
        private Lazy<IRepository<UserRole>> userRoles;

        /// <summary>
        /// Users table
        /// </summary>
        private Lazy<IRepository<User>> users;

        /// <summary>
        /// Gets or sets roles table
        /// </summary>
        public IRepository<Role> Roles
        {
            get => roles.Value;
            set
            {
                roles = new Lazy<IRepository<Role>>(() => value);
            }
        }

        /// <summary>
        /// Gets or sets user roles table
        /// </summary>
        public IRepository<UserRole> UserRoles
        {
            get => userRoles.Value;
            set
            {
                userRoles = new Lazy<IRepository<UserRole>>(() => value);
            }
        }

        /// <summary>
        /// Gets or sets user users table
        /// </summary>
        public IRepository<User> Users
        {
            get => users.Value;
            set
            {
                users = new Lazy<IRepository<User>>(() => value);
            }
        }
    }
}
