﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Timer.DAL.Timer.DAL.Entities
{

    public class User:IdentityUser<int>,IUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<Preset> Presets { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<Tasks> Tasks { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        public User()
        {
            this.PhoneNumberConfirmed = false;
            this.TwoFactorEnabled = false;
            this.LockoutEnabled = false;
            this.AccessFailedCount = 0;
            this.EmailConfirmed = false;
        }
    }
}
