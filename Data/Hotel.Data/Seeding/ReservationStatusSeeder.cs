namespace Hotel.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Services.Data;
    using Microsoft.Extensions.DependencyInjection;

    public class ReservationStatusSeeder : ISeeder
    {
        public async Task SeedAsync(HotelDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ReservationStatuses.Any())
            {
                return;
            }

            var reservationStatusesService = serviceProvider.GetRequiredService<IReservationStatusesService>();

            var roomStatuses = new string[]
            {
                "Pending",
                "Confirmed",
            };

            await reservationStatusesService.CreateAllAsync(roomStatuses);
        }
    }
}
