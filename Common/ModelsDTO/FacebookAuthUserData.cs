using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ModelsDTO
{
    public class FacebookAuthUserData
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}
