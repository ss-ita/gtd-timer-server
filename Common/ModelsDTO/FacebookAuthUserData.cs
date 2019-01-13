using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ModelsDTO
{
    public class FacebookAuthUserData: BaseAuthUserData
    {
        [JsonProperty("email")]
        public override string Email { get; set; }
        [JsonProperty("first_name")]
        public override string FirstName { get; set; }
        [JsonProperty("last_name")]
        public override string LastName { get; set; }
    }
}
