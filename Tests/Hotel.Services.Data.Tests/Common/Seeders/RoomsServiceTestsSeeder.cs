namespace Hotel.Services.Data.Tests.Common.Seeders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;

    public class RoomsServiceTestsSeeder
    {
        public async Task SeedRoomAsync(HotelDbContext context)
        {
            var room = new Room()
            {
                RoomNumber = "Test-1",
                RoomType = new RoomType { Name = "TestType" },
                Description = "TestDescription",
            };

            await context.Rooms.AddAsync(room);

            await context.SaveChangesAsync();
        }

        public async Task SeedRoomsAsync(HotelDbContext context)
        {
            await context.RoomTypes.AddAsync(new RoomType() { Name = "TestType1" });
            await context.RoomTypes.AddAsync(new RoomType() { Name = "TestType2" });
            await context.RoomTypes.AddAsync(new RoomType() { Name = "TestType3" });

            await context.SaveChangesAsync();

            var rooms = new List<Room>()
            {
                new Room()
                {
                    RoomNumber = "Test-1",
                    RoomType = context.RoomTypes.First(x => x.Name == "TestType1"),
                    Description = "TestDescription1",
                },
                new Room()
                {
                    RoomNumber = "Test-2",
                    RoomType = context.RoomTypes.First(x => x.Name == "TestType2"),
                    Description = "TestDescription2",
                },
                new Room()
                {
                    RoomNumber = "Test-3",
                    RoomType = context.RoomTypes.First(x => x.Name == "TestType3"),
                    Description = "TestDescription3",
                },
            };

            await context.Rooms.AddRangeAsync(rooms);
            await context.SaveChangesAsync();
        }

        public async Task SeedDataForEditAsyncMethodAsync(HotelDbContext context)
        {
            var room = new Room()
            {
                RoomNumber = "Test",
                RoomType = new RoomType { Name = "TestType" },
                Description = "TestDescriptionEdit",
            };

            await context.Rooms.AddAsync(room);
            await context.RoomTypes.AddAsync(new RoomType { Name = "TestType" });
            await context.RoomTypes.AddAsync(new RoomType { Name = "TestTypeEdited" });
            await context.SaveChangesAsync();
        }

        public async Task SeedDataForAddAsyncMethodAsync(HotelDbContext context)
        {
            await context.RoomTypes.AddAsync(new RoomType { Name = "TestType" });
            await context.SaveChangesAsync();
        }
    }
}
