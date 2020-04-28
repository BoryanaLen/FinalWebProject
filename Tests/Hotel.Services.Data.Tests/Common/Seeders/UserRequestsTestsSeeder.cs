namespace Hotel.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;

    public class UserRequestsTestsSeeder
    {
        public async Task SeedUserRequestAsync(HotelDbContext context)
        {
            var userRequest = new UserRequest
            {
                Title = "Title",
                Content = "Content",
                Email = "email",
                RequestDate = DateTime.Now,
                Seen = false,
            };

            await context.UserRequests.AddAsync(userRequest);

            await context.SaveChangesAsync();
        }

        public async Task SeedUserRequestsAsync(HotelDbContext context)
        {
            var userRequests = new List<UserRequest>()
            {
                new UserRequest()
                {
                    Title = "Title",
                    Content = "Content",
                    Email = "email",
                    RequestDate = DateTime.Now,
                },
                new UserRequest()
                {
                    Title = "Title",
                    Content = "Content",
                    Email = "email",
                    RequestDate = DateTime.Now,
                },
                new UserRequest()
                {
                   Title = "Title",
                   Content = "Content",
                   Email = "email",
                   RequestDate = DateTime.Now,
                },
            };

            await context.UserRequests.AddRangeAsync(userRequests);

            await context.SaveChangesAsync();
        }
    }
}
