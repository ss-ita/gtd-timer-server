using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace gtdtimer.Timer.DAL.Entities
{
    public class TimerContext:IdentityDbContext<User,Role,int>
    {
        public TimerContext(DbContextOptions<TimerContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Preset> Presets { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasData(
                new User
                {   
                    Id = 77,
                    FirstName = "Alice",
                    LastName = "Smith",
                    PasswordHash = "1234567",
                    Email = "example1@gmail.com"
                },
                new User
                {
                    Id = 41,
                    FirstName = "Bob",
                    LastName = "Johns",
                    PasswordHash = "54237829",
                    Email = "example2@gmail.com"
                },
                new User
                {
                    Id = 31,
                    FirstName = "Sam",
                    LastName = "Paul",
                    PasswordHash = "0978687687",
                    Email = "example3@gmail.com"
                }
            );
        }
    }
}
