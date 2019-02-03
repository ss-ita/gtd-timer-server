//-----------------------------------------------------------------------
// <copyright file="UserRole.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.AspNetCore.Identity;

namespace GtdTimerDAL.Entities
{
    /// <summary>
    /// Fields of user role table
    /// </summary>
    public class UserRole : IdentityUserRole<int>
    {
        /// <summary>
        /// Gets or sets user foreign key
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets roles foreign key
        /// </summary>
        public Role Role { get; set; }
    }
}
