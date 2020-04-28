namespace Hotel.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Common;
    using Hotel.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UserInRoleSeeder : ISeeder
    {
        public async Task SeedAsync(HotelDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<HotelUser>>();

            await SeedUserInRoleAsync(userManager);
        }

        private static async Task SeedUserInRoleAsync(UserManager<HotelUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new HotelUser
                {
                    FirstName = "AdminFName",
                    LastName = "AdminLName",
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    Address = "Plovdiv",
                    PhoneNumber = "123456789",
                };

                var password = "123456";

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }

                var userTwo = new HotelUser
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    UserName = "ivan@ivan.com",
                    Email = "ivan@ivan.com",
                    Address = "Plovdiv",
                    PhoneNumber = "123456789",
                };

                var passwordTwo = "123456";

                var resultTwo = await userManager.CreateAsync(userTwo, passwordTwo);

                if (resultTwo.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
                }
            }
        }
    }
}
