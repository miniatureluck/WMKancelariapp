using Microsoft.AspNetCore.Identity;
using WMKancelariapp.Models;

namespace WMKancelariapp.Data
{
    public static class Seed
    {
        public static async Task Initialize(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            await context.Database.EnsureCreatedAsync();

            if (context.Users.Any())
            {
                return;
            }

            await roleManager.CreateAsync(new IdentityRole { Name = "SysAdmin" });
            var admin = new User { Email = "admin@example.com", UserName = "admin@example.com", EmailConfirmed = true };
            await userManager.CreateAsync(admin, "123qwe!@#QWE");
            await userManager.AddToRoleAsync(admin, "SysAdmin");

            var sampleCase = new Case()
            {
                Name = "Sample case",
                Description = "Just a seeded case"
            };

            var sampleClient = new Client
            {
                Email = "client@example.com",
                Address = "Lokalna 2",
                AssignedUser = admin,
                Description = "starting client",
                Location = "warsaw",
                PostCode = "22-222",
                Created = DateTime.Now,
                Surname = "Kowalski",
                Name = "Marian",
                Phone = "333-444-555",
                TaxIdNumber = "231241433"
            };

            var sampleTaskType = new TaskType
            {
                Created = DateTime.Now,
                Name = "Rozmowa telefoniczna"
            };

            var samplePrice = new HourlyPrice
            {
                Case = sampleCase,
                Price = 100,
                Created = DateTime.Now,
                TaskType = sampleTaskType
            };


            var sampleTask = new UserTask
            {
                Client = sampleClient,
                Duration = TimeSpan.FromSeconds(1).Ticks,
                Created = DateTime.Now,
                Description = "sample description",
                User = admin,
                HourlyPrice = samplePrice,
                TaskType = sampleTaskType,
                Case = sampleCase
            };

            samplePrice.UserTasks.Add(sampleTask);

            context.Clients.Add(sampleClient);
            context.HourlyPrices.Add(samplePrice);
            context.Tasks.Add(sampleTask);

            await context.SaveChangesAsync();
        }
    }
}
