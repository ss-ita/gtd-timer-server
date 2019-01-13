﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Timer.DAL.Timer.DAL.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
