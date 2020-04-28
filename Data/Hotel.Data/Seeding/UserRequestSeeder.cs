namespace Hotel.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Microsoft.Extensions.DependencyInjection;

    public class UserRequestSeeder : ISeeder
    {
        public async Task SeedAsync(HotelDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.UserRequests.Any())
            {
                return;
            }

            var userRequestService = serviceProvider.GetRequiredService<IUserRequestsService>();

            var requests = new UserRequest[]
            {
                new UserRequest
                {
                    Title = "Hello - One",
                    Content = "Hello - One",
                    Email = "email@email.com",
                    Seen = false,
                    RequestDate = DateTime.Now,
                },
                new UserRequest
                {
                    Title = "Hello - Two",
                    Content = "Hello - Two",
                    Email = "email@email.com",
                    Seen = false,
                    RequestDate = DateTime.Now,
                },
                new UserRequest
                {
                    Title = "Hello - Three",
                    Content = "Hello - Three",
                    Email = "email@email.com",
                    Seen = true,
                    RequestDate = DateTime.Now,
                },
            };

            foreach (var request in requests)
            {
                await userRequestService.AddUserRequestAsync(request);
            }
        }
    }
}
