namespace Hotel.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.Rooms;
    using Microsoft.Extensions.DependencyInjection;

    public class RoomsSeeder : ISeeder
    {
        public async Task SeedAsync(HotelDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Rooms.Any())
            {
                return;
            }

            var roomsService = serviceProvider.GetRequiredService<IRoomsService>();

            var hotelsService = serviceProvider.GetRequiredService<IHotelsService>();

            var roomTypesService = serviceProvider.GetRequiredService<IRoomTypesService>();

            var hotel = hotelsService.GetHotelByName("Hotel Boryana");

            var rooms = new AddRoomInputModel[]
            {
                new AddRoomInputModel
                {
                    RoomNumber = "S-1",
                    RoomTypeId = roomTypesService.GetRoomTypeByName("Single room").Id,
                    Description = "Single room",
                    HotelDataId = hotel.Id,
                },
                new AddRoomInputModel
                {
                    RoomNumber = "D-1",
                    RoomTypeId = roomTypesService.GetRoomTypeByName("Double room").Id,
                    Description = "Double room",
                    HotelDataId = hotel.Id,
                },
                new AddRoomInputModel
                {
                    RoomNumber = "St-1",
                    RoomTypeId = roomTypesService.GetRoomTypeByName("Studio").Id,
                    Description = "Studio",
                    HotelDataId = hotel.Id,
                },
                new AddRoomInputModel
                {
                    RoomNumber = "A-1",
                    RoomTypeId = roomTypesService.GetRoomTypeByName("Apartment").Id,
                    Description = "Apartment",
                    HotelDataId = hotel.Id,
                },
            };

            foreach (var roomModel in rooms)
            {
                Room room = AutoMapperConfig.MapperInstance.Map<Room>(roomModel);

                await roomsService.AddRoomAsync(room);
            }
        }
    }
}
