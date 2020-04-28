namespace Hotel.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class ReservationsSeeder : ISeeder
    {
        public async Task SeedAsync(HotelDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<HotelUser>>();

            await SeedReservationsAsync(dbContext, serviceProvider, userManager);
        }

        private static async Task SeedReservationsAsync(
            HotelDbContext dbContext,
            IServiceProvider serviceProvider,
            UserManager<HotelUser> userManager)
        {
            if (dbContext.Reservations.Any())
            {
                return;
            }

            var roomsService = serviceProvider.GetRequiredService<IRoomsService>();
            var roomTypesService = serviceProvider.GetRequiredService<IRoomTypesService>();
            var reservationsService = serviceProvider.GetRequiredService<IReservationsService>();
            var paymentTypesService = serviceProvider.GetRequiredService<IPaymentTypesService>();
            var reservationStatusesService = serviceProvider.GetRequiredService<IReservationStatusesService>();

            var reservationStatusId = reservationStatusesService.GetReserVationStatusByName("Pending").Id;
            var room = roomsService.GetRoomByRoomTypeName("Single room");
            RoomType roomType = await roomTypesService.GetRoomTypeByIdAsync(room.RoomTypeId);
            PaymentType paymentType = await paymentTypesService.GetPaymentTypeByNameAsync("Direct bank transfer");
            HotelUser user = await userManager.FindByNameAsync("ivan@ivan.com");

            var reservationOne = new Reservation
            {
                UserId = user.Id,
                StartDate = new DateTime(2020, 1, 10, 14, 0, 0),
                EndDate = new DateTime(2020, 1, 20, 12, 0, 0),
                Adults = 1,
                Kids = 0,
                ReservationStatusId = reservationStatusId,
                PaymentTypeId = paymentType.Id,
                PricePerDay = roomType.Price,
                TotalDays = (int)(new DateTime(2020, 1, 20) - new DateTime(2020, 1, 10)).TotalDays,
                TotalAmount = ((int)(new DateTime(2020, 1, 20) - new DateTime(2020, 1, 10)).TotalDays) * roomType.Price,
                CreatedOn = DateTime.UtcNow,
            };

            var reservationRoomOne = new ReservationRoom
            {
                RoomId = room.Id,
                ReservationId = reservationOne.Id,
            };

            reservationOne.ReservationRooms = new List<ReservationRoom> { reservationRoomOne };

            var reservationTwo = new Reservation
            {
                UserId = user.Id,
                StartDate = new DateTime(2020, 2, 08, 14, 0, 0),
                EndDate = new DateTime(2020, 2, 20, 12, 0, 0),
                Adults = 1,
                Kids = 0,
                ReservationStatusId = reservationStatusId,
                PaymentTypeId = paymentType.Id,
                PricePerDay = roomType.Price,
                TotalDays = (int)(new DateTime(2020, 2, 20) - new DateTime(2020, 2, 08)).TotalDays,
                TotalAmount = ((int)(new DateTime(2020, 2, 20) - new DateTime(2020, 2, 08)).TotalDays) * roomType.Price,
                CreatedOn = DateTime.UtcNow,
            };

            var reservationRoomTwo = new ReservationRoom
            {
                RoomId = room.Id,
                ReservationId = reservationTwo.Id,
            };

            reservationTwo.ReservationRooms = new List<ReservationRoom> { reservationRoomTwo };

            var reservationThree = new Reservation
            {
                UserId = user.Id,
                StartDate = DateTime.UtcNow.AddDays(-1),
                EndDate = DateTime.UtcNow.AddDays(5),
                Adults = 1,
                Kids = 0,
                ReservationStatusId = reservationStatusId,
                PaymentTypeId = paymentType.Id,
                PricePerDay = roomType.Price,
                TotalDays = (int)(DateTime.UtcNow.AddDays(5) - DateTime.UtcNow.AddDays(-1)).TotalDays,
                TotalAmount = ((int)(DateTime.UtcNow.AddDays(5) - DateTime.UtcNow.AddDays(-1)).TotalDays) * roomType.Price,
                CreatedOn = DateTime.UtcNow,
            };

            var reservationRoomThree = new ReservationRoom
            {
                RoomId = room.Id,
                ReservationId = reservationThree.Id,
            };
            reservationThree.ReservationRooms = new List<ReservationRoom> { reservationRoomThree };

            var reservationFour = new Reservation
            {
                UserId = user.Id,
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow.AddDays(-2),
                Adults = 3,
                Kids = 1,
                ReservationStatusId = reservationStatusId,
                PaymentTypeId = paymentType.Id,
                PricePerDay = roomType.Price,
                TotalDays = (int)(DateTime.UtcNow.AddDays(-2) - DateTime.UtcNow.AddDays(-7)).TotalDays,
                TotalAmount = (int)(DateTime.UtcNow.AddDays(-2) - DateTime.UtcNow.AddDays(-7)).TotalDays * roomType.Price,
                CreatedOn = DateTime.UtcNow,
            };

            var reservationRoomFour = new ReservationRoom
            {
                RoomId = room.Id,
                ReservationId = reservationFour.Id,
            };
            reservationFour.ReservationRooms = new List<ReservationRoom> { reservationRoomFour };

            await reservationsService.AddReservationAsync(reservationOne);
            await reservationsService.AddReservationAsync(reservationTwo);
            await reservationsService.AddReservationAsync(reservationThree);
            await reservationsService.AddReservationAsync(reservationFour);
        }
    }
}
