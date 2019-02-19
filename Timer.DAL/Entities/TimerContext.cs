//-----------------------------------------------------------------------
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
            Database.Migrate();
            Database.EnsureCreated();
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
        /// Gets or sets set of preset tasks
        /// </summary>
        public virtual DbSet<PresetTasks> PresetTasks { get; set; }

        /// <summary>
        /// Gets or sets set of records
        /// </summary>
        public virtual DbSet<Record> Records { get; set; }

        /// <summary>
        /// Gets or sets set of alarms
        /// </summary>
        public virtual DbSet<Alarm> Alarms { get; set; }

        /// <summary>
        /// Gets or sets set of tokens
        /// </summary>
        public virtual DbSet<Token> Tokens { get; set; }

        /// <summary>
        /// method used when creating database models
        /// </summary>
        /// <param name="builder"> model builder instance </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}
