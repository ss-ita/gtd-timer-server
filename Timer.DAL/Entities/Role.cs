//-----------------------------------------------------------------------
// <copyright file="Role.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GtdTimerDAL.Entities
{
    /// <summary>
    /// Fields of role table
    /// </summary>
    public class Role : IdentityRole<int>
    {
        /// <summary>
        /// Gets or sets UserRoles column
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
