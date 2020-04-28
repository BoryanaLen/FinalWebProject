namespace Hotel.Data.Seeding
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.RoomTypes;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class RoomTypeSeeder : ISeeder
    {
        public async Task SeedAsync(HotelDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.RoomTypes.Any())
            {
                return;
            }

            var roomTypesService = serviceProvider.GetRequiredService<IRoomTypesService>();
            var cloudinaryService = serviceProvider.GetRequiredService<ICloudinaryService>();
            var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            await SeedRoomTypesAsync(cloudinaryService, roomTypesService, env);
        }

        private static async Task SeedRoomTypesAsync(
            ICloudinaryService cloudinaryService,
            IRoomTypesService roomTypesService,
            IWebHostEnvironment env)
        {
            var roomTypes = new AddRoomTypeInputModel[]
            {
                new AddRoomTypeInputModel
                {
                    Name = "Single room",
                    Price = 100,
                    CapacityAdults = 1,
                    CapacityKids = 0,
                    Image = "pictures/single-room.jpg",
                    Description = @"Suitable for one adult; LCD TV with cable TV;
Free Wi-Fi access; Individually controlled air conditioning system; 
Separate bathroom and toilet; Non-smoking room; Telephone Hair dryer;",
                },
                new AddRoomTypeInputModel
                {
                    Name = "Double room",
                    Price = 150,
                    CapacityAdults = 2,
                    CapacityKids = 1,
                    Image = "pictures/double room.jpg",
                    Description = @"Panoramic window room; Suitable for two adults; 
Individually controlled air conditioning system; LCD TV with cable television; 
Free Wi-Fi Separate beds; Separate bathroom and toilet; Non-smoking room; 
Mini bar; Telephone Hair dryer;",
                },
                new AddRoomTypeInputModel
                {
                    Name = "Studio",
                    Price = 200,
                    CapacityAdults = 3,
                    CapacityKids = 1,
                    Image = "pictures/club-floor-room.jpg",
                    Description = @"Each of the spacious studios has a unique vision and interior.
Some of the studios have terraces with wonderful views, as well as baths in the bathrooms. 
The studios at the hotel are equipped with bedrooms, a dressing table, 
a living room with a sofa, armchairs and a coffee table, and the equipment 
includes multi split air conditioners, digital TV, telephone, 
mini bar and wireless internet. Each of the studios bathrooms is supplied with
healing mineral water.",
                },
                new AddRoomTypeInputModel
                {
                    Name = "Apartment",
                    Price = 250,
                    CapacityAdults = 4,
                    CapacityKids = 1,
                    Image = "pictures/suits.jpg",
                    Description = @"Stylishly furnished apartments with an area of ​​90 sq.m. consisting
of a separate living and sleeping area and an extra spacious terrace, 
revealing a splendid panoramic view of the mountain.",
                },
            };

            foreach (var type in roomTypes)
            {
                var path = env.WebRootFileProvider.GetFileInfo(type.Image)?.PhysicalPath;

                using (var stream = File.OpenRead(path))
                {
                    var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/jpg",
                    };

                    type.RoomImage = file;

                    var photoUrl = await cloudinaryService.UploadPhotoAsync(
                     type.RoomImage,
                     $"Room - {type.Name}",
                     "Hotel_room_types_photos");

                    type.Image = photoUrl;
                }

                RoomType roomType = AutoMapperConfig.MapperInstance.Map<RoomType>(type);

                await roomTypesService.AddRoomTypeAsync(roomType);
            }
        }
    }
}
