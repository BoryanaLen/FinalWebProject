namespace Hotel.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Microsoft.Extensions.DependencyInjection;

    public class HotelDataSeeder : ISeeder
    {
        public async Task SeedAsync(HotelDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Hotels.Any())
            {
                return;
            }

            var hotelService = serviceProvider.GetRequiredService<IHotelsService>();

            var hotel = new HotelData
            {
                Name = "Hotel Boryana",
                Address = "Velingrad, Bulgaria",
                UniqueIdentifier = "123456789",
                Owner = "Boryana",
                Manager = "Boryana",
                PhoneNumber = "123456789",
            };

            await hotelService.AddRHotelAsync(hotel);
        }
    }
}
