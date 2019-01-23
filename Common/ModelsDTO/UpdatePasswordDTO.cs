//-----------------------------------------------------------------------
// <copyright file="UpdatePasswordDto.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

using GtdCommon.Constant;

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for Update Password model
    /// </summary>
    public class UpdatePasswordDto
    {
        /// <summary>
        /// Gets or sets a value of old password property
        /// </summary>
        [Required]
        public string PasswordOld { get; set; }

        /// <summary>
        /// Gets or sets a value of new password property
        /// </summary>
        [Required]
        [MinLength(8)]
        [RegularExpression(Constants.PasswordRegularExpression, ErrorMessage = Constants.PasswordInvalidMessage)]
        public string PasswordNew { get; set; }

        /// <summary>
        /// Gets or sets a value of password confirmation property
        /// </summary>
        [Required]
        [Compare("PasswordNew")]
        public string PasswordConfirm { get; set; }
    }
}
