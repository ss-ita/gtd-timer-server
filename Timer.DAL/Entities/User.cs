//-----------------------------------------------------------------------
// <copyright file="User.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace GtdTimerDAL.Entities
{
    /// <summary>
    /// Fields of user table
    /// </summary>
    public class User : IdentityUser<int>, IUser<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        public User()
        {
            PhoneNumberConfirmed = false;
            TwoFactorEnabled = false;
            LockoutEnabled = false;
            AccessFailedCount = 0;
            EmailConfirmed = false;
        }

        /// <summary>
        /// Gets or sets FirstName column
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets LastName column
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Foreign key to Presets table
        /// </summary>
        public IEnumerable<Preset> Presets { get; set; }

        /// <summary>
        /// Gets or sets Foreign key to Messages table
        /// </summary>
        public IEnumerable<Message> Messages { get; set; }

        /// <summary>
        /// Gets or sets Foreign key to Tasks table
        /// </summary>
        public IEnumerable<Tasks> Tasks { get; set; }

        /// <summary>
        /// Gets or sets Foreign key to UserRoles table
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
