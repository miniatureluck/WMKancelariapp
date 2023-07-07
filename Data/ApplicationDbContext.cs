using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WMKancelariapp.Models;

namespace WMKancelariapp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<HourlyPrice> HourlyPrices { get; set; }
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = _configuration.GetSection("ConnectionStrings").GetSection("Primary").Value;
            optionsBuilder.UseSqlServer(config, b => b.MigrationsAssembly("WMKancelariapp"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Case>()
                .HasOne(x => x.AssignedUser).WithMany(x => x.Cases);
            builder.Entity<Case>()
                .HasOne(x => x.Client).WithMany(x => x.Cases)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<User>()
                .HasMany(x => x.Clients).WithOne(x => x.AssignedUser)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<User>()
                .HasMany(x => x.Tasks).WithOne(x => x.User);
            builder.Entity<User>()
                .HasMany(x => x.HourlyPrices).WithOne(x => x.User);

            builder.Entity<TaskType>()
                .HasMany(x => x.Tasks).WithOne(x => x.TaskType);
            builder.Entity<TaskType>()
                .HasIndex(x => x.Name).IsUnique();
            builder.Entity<TaskType>()
                .HasMany(x => x.HourlyPrices).WithOne(x => x.TaskType);

            builder.Entity<UserTask>()
                .HasOne(x => x.Client).WithMany(x => x.Tasks)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<UserTask>()
                .HasOne(x => x.Case).WithMany(x => x.Tasks);
            builder.Entity<UserTask>()
                .HasOne(x => x.HourlyPrice).WithMany(x => x.UserTasks);


        }
    }
}