using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace gtdtimer.Timer.DAL.Entities
{
    public class User:IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<Preset> Presets { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
    }
}
