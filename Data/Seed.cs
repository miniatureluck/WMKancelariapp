using Microsoft.AspNetCore.Identity;
using WMKancelariapp.Models;

namespace WMKancelariapp.Data
{
    public static class Seed
    {
        public async static Task Initialize(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            await context.Database.EnsureCreatedAsync();

            if (context.Users.Any())
            {
                return;
            }

            await roleManager.CreateAsync(new IdentityRole { Name = "SysAdmin" });
            var admin = new User { Email = "admin@example.com", UserName = "admin@example.com", EmailConfirmed = true};
            await userManager.CreateAsync(admin, "123qwe!@#QWE");
            await userManager.AddToRoleAsync(admin, "SysAdmin");

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

            var samplePrice = new HourlyPrice
            {
                Client = sampleClient,
                User = admin,
                Price = 100,
                Created = DateTime.Now,
            };

            var sampleTask = new UserTask
            {
                Client = sampleClient,
                Duration = TimeSpan.FromSeconds(1),
                Created = DateTime.Now,
                Description = "sample description",
                User = admin,
                HourlyPrice = samplePrice,
                TaskType = new TaskType
                {
                    Created = DateTime.Now,
                    Name = "Rozmowa telefoniczna"
                }
            };

            samplePrice.Task = sampleTask;

            context.Clients.Add(sampleClient);
            context.HourlyPrices.Add(samplePrice);
            context.Tasks.Add(sampleTask);

            await context.SaveChangesAsync();
        }
    }
}
