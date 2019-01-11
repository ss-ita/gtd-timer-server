using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ModelsDTO
{
    public class GoogleAuthUserData: BaseAuthUserData
    {
        [JsonProperty("email")] 
        public override string Email { get; set; }
        [JsonProperty("given_name")]
        public override string FirstName { get; set; }
        [JsonProperty("family_name")]
        public override string LastName { get; set; }
    }
}
