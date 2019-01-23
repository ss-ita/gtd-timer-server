//-----------------------------------------------------------------------
// <copyright file="LoginDto.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

using GtdCommon.Constant;

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for Login model
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets a value of Email property
        /// </summary>
        [Required(ErrorMessage = Constants.RequiredUser)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value of Password property
        /// </summary>
        [Required(ErrorMessage = Constants.RequiredUser)]
        public string Password { get; set; }
    }
}
