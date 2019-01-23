//-----------------------------------------------------------------------
// <copyright file="FacebookAuthUserData.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using Newtonsoft.Json;

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for Facebook Authorization User Data model
    /// </summary>
    public class FacebookAuthUserData : BaseAuthUserData
    {
        /// <summary>
        /// Gets or sets a value of Email property
        /// </summary>
        [JsonProperty("email")]
        public override string Email { get; set; }

        /// <summary>
        /// Gets or sets a value of First name property
        /// </summary>
        [JsonProperty("first_name")]
        public override string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a value of Last name property
        /// </summary>
        [JsonProperty("last_name")]
        public override string LastName { get; set; }
    }
}
