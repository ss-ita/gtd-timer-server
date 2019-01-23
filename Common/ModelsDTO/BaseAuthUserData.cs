//-----------------------------------------------------------------------
// <copyright file="BaseAuthUserData.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for Base Authorization User Data model
    /// </summary>
    public class BaseAuthUserData
    {
        /// <summary>
        /// Gets or sets a value of Email property
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets a value of First name property
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a value of Last name property
        /// </summary>
        public virtual string LastName { get; set; }
    }
}
