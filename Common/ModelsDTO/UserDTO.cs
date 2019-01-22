//-----------------------------------------------------------------------
// <copyright file="UserDto.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

using GtdCommon.Constant;

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for User model
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets a value of First name property
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a value of Last name property
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a value of Email property
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value of Password property
        /// </summary>
        [Required]
        [MinLength(8)]
        [RegularExpression(Constants.PasswordRegularExpression, ErrorMessage = Constants.PasswordInvalidMessage)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value of password confirmation property property
        /// </summary>
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
