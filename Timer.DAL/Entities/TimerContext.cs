﻿//-----------------------------------------------------------------------
// <copyright file="TimerContext.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GtdTimerDAL.Entities
{
    /// <summary>
    /// Class for context of database
    /// </summary>
    public class TimerContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimerContext" /> class.
        /// </summary>
        /// <param name="options"> Database context options </param>
        public TimerContext(DbContextOptions<TimerContext> options) : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerContext" /> class.
        /// </summary>
        public TimerContext()
        {
        }

        /// <summary>
        /// Gets or sets set of users
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets set of roles
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets set of user roles
        /// </summary>
        public virtual DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets set of presets
        /// </summary>
        public virtual DbSet<Preset> Presets { get; set; }

        /// <summary>
        /// Gets or sets set of messages
        /// </summary>
        public virtual DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Gets or sets set of tasks
        /// </summary>
        public virtual DbSet<Tasks> Tasks { get; set; }

        /// <summary>
        /// Gets or sets set of timers
        /// </summary>
        public virtual DbSet<Timer> Timers { get; set; }

        /// <summary>
        /// method used when creating database models
        /// </summary>
        /// <param name="builder"> model builder instance </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}