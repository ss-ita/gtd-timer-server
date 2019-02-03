//-----------------------------------------------------------------------
// <copyright file="RoleDto.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for Role model
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Gets or sets a value of Email property
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value of Role property
        /// </summary>
        [Required]
        public string Role { get; set; }
    }
}
