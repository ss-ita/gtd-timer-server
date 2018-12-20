using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Timer.DAL.Timer.DAL.Entities
{
    public class TimerContext:IdentityDbContext<User,Role,int>
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
        public virtual DbSet<Preset> Presets { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Timer> Timers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
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
