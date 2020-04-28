namespace Hotel.Services.Data.Tests.Common
{
    using System;

    using Hotel.Data;
    using Microsoft.EntityFrameworkCore;

    public class HotelDbContextInMemoryFactory
    {
        public static HotelDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<HotelDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new HotelDbContext(options);
        }
    }
}
