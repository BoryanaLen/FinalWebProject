namespace Hotel.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;

    public class RoomTypesServiceTestsSeeder
    {
        public async Task SeedRoomTypeAsync(HotelDbContext context)
        {
            var roomType = new RoomType()
            {
                Name = "Test-1",
                Price = 100,
                CapacityAdults = 1,
                CapacityKids = 0,
                Image = "test1.jpg",
                Description = "Description1",
            };

            await context.RoomTypes.AddAsync(roomType);

            await context.SaveChangesAsync();
        }

        public async Task SeedRoomTypesAsync(HotelDbContext context)
        {
            var roomTypes = new List<RoomType>()
            {
                new RoomType()
                {
                     Name = "Test-1",
                     Price = 100,
                     CapacityAdults = 1,
                     CapacityKids = 0,
                     Image = "test1.jpg",
                     Description = "Description1",
                },
                new RoomType()
                {
                     Name = "Test-2",
                     Price = 150,
                     CapacityAdults = 2,
                     CapacityKids = 1,
                     Image = "test2.jpg",
                     Description = "Description2",
                },
                new RoomType()
                {
                     Name = "Test-3",
                     Price = 180,
                     CapacityAdults = 1,
                     CapacityKids = 0,
                     Image = "test1.jpg",
                     Description = "Description1",
                },
            };

            await context.RoomTypes.AddRangeAsync(roomTypes);

            await context.SaveChangesAsync();
        }
    }
}
