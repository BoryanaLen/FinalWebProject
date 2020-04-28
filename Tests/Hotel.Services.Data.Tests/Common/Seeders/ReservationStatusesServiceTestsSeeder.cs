namespace Hotel.Services.Data.Tests.Common.Seeders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;

    public class ReservationStatusesServiceTestsSeeder
    {
        public async Task SeedReservationStatusAsync(HotelDbContext context)
        {
            var reservationStatus = new ReservationStatus()
            {
                Name = "Test-1",
            };

            await context.ReservationStatuses.AddAsync(reservationStatus);

            await context.SaveChangesAsync();
        }

        public async Task SeedReservationStatusesAsync(HotelDbContext context)
        {
            var reservationStatuses = new List<ReservationStatus>()
            {
                new ReservationStatus()
                {
                    Name = "Test-1",
                },
                new ReservationStatus()
                {
                    Name = "Test-2",
                },
                new ReservationStatus()
                {
                    Name = "Test-3",
                },
            };

            await context.ReservationStatuses.AddRangeAsync(reservationStatuses);

            await context.SaveChangesAsync();
        }
    }
}
