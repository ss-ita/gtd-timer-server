using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace gtdtimer.Timer.DAL.Entities
{
    public class User:IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Preset> Presets { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
