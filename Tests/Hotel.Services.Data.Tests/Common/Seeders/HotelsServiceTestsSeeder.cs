namespace Hotel.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;

    public class HotelsServiceTestsSeeder
    {
        public async Task SeedHotelTypeAsync(HotelDbContext context)
        {
            var hotel = new HotelData
            {
                Name = "Test",
                Address = "Bulgaria",
                UniqueIdentifier = "123456789",
                Owner = "Owner",
                Manager = "Manager",
                PhoneNumber = "123456789",
            };

            await context.Hotels.AddAsync(hotel);

            await context.SaveChangesAsync();
        }

    }
}
