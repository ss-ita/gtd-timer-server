using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ModelsDTO
{
    public class GoogleAuthUserData
    {
        [JsonProperty("email")] 
        public string Email { get; set; }
        [JsonProperty("given_name")]
        public string FirstName { get; set; }
        [JsonProperty("family_name")]
        public string LastName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
