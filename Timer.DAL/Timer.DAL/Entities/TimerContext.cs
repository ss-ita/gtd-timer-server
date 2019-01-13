using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Timer.DAL.Timer.DAL.Entities
{
    public class TimerContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public TimerContext(DbContextOptions<TimerContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        public TimerContext()
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Preset> Presets { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Timer> Timers { get; set; }

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
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 300,
                    FirstName = "Alice",
                    LastName = "Smith",
                    PasswordHash = "1234567",
                    Email = "example33@gmail.com"
                },
                new User
                {
                    Id = 301,
                    FirstName = "Bob",
                    LastName = "Johns",
                    PasswordHash = "54237829",
                    Email = "example34@gmail.com"
                },
                new User
                {
                    Id = 302,
                    FirstName = "Sam",
                    LastName = "Paul",
                    PasswordHash = "0978687687",
                    Email = "example35@gmail.com"
                }
            );
        }
    }
}
