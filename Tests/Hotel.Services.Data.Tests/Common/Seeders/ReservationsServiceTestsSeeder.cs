namespace Hotel.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;

    public class ReservationsServiceTestsSeeder
    {
        public async Task SeedReservationAsync(HotelDbContext context)
        {
            await this.SeedDataForAddAsyncMethodAsync(context);

            var reservation = new Reservation()
            {
                UserId = context.Users.First().Id,
                StartDate = new DateTime(2020, 4, 4),
                EndDate = new DateTime(2020, 4, 8),
                Adults = 2,
                Kids = 1,
                ReservationStatusId = context.ReservationStatuses.First().Id,
                PaymentTypeId = context.PaymentTypes.First().Id,
                TotalAmount = 1000,
                PricePerDay = 250,
                TotalDays = 4,
                AdvancedPayment = 300,
                ReservationRooms = new List<ReservationRoom> { new ReservationRoom { RoomId = context.Rooms.First().Id } },
            };

            await context.Reservations.AddAsync(reservation);

            await context.SaveChangesAsync();
        }

        public async Task SeedReservationForGetRoomsArrivalsAsync(HotelDbContext context)
        {
            await this.SeedDataForAddAsyncMethodAsync(context);

            var reservation = new Reservation()
            {
                UserId = context.Users.First().Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3),
                Adults = 2,
                Kids = 1,
                ReservationStatusId = context.ReservationStatuses.First().Id,
                PaymentTypeId = context.PaymentTypes.First().Id,
                TotalAmount = 1000,
                PricePerDay = 250,
                TotalDays = 4,
                AdvancedPayment = 300,
                ReservationRooms = new List<ReservationRoom> { new ReservationRoom { RoomId = context.Rooms.First().Id } },
            };

            await context.Reservations.AddAsync(reservation);

            await context.SaveChangesAsync();
        }

        public async Task SeedReservationForGetReservedRoomsAsync(HotelDbContext context)
        {
            await this.SeedDataForAddAsyncMethodAsync(context);

            var reservation = new Reservation()
            {
                UserId = context.Users.First().Id,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(3),
                Adults = 2,
                Kids = 1,
                ReservationStatusId = context.ReservationStatuses.First().Id,
                PaymentTypeId = context.PaymentTypes.First().Id,
                TotalAmount = 1000,
                PricePerDay = 250,
                TotalDays = 4,
                AdvancedPayment = 300,
                ReservationRooms = new List<ReservationRoom> { new ReservationRoom { RoomId = context.Rooms.First().Id } },
            };

            await context.Reservations.AddAsync(reservation);

            await context.SaveChangesAsync();
        }

        public async Task SeedReservationForGetRoomsDeparture(HotelDbContext context)
        {
            await this.SeedDataForAddAsyncMethodAsync(context);

            var reservation = new Reservation()
            {
                UserId = context.Users.First().Id,
                StartDate = DateTime.Now.AddDays(-4),
                EndDate = DateTime.Now,
                Adults = 2,
                Kids = 1,
                ReservationStatusId = context.ReservationStatuses.First().Id,
                PaymentTypeId = context.PaymentTypes.First().Id,
                TotalAmount = 1000,
                PricePerDay = 250,
                TotalDays = 4,
                AdvancedPayment = 300,
                ReservationRooms = new List<ReservationRoom> { new ReservationRoom { RoomId = context.Rooms.First().Id } },
            };

            await context.Reservations.AddAsync(reservation);

            await context.SaveChangesAsync();
        }

        public async Task SeedReservationsAsync(HotelDbContext context)
        {
            await this.SeedDataForAddAsyncMethodAsync(context);

            var reservations = new List<Reservation>()
            {
                new Reservation()
                {
                    UserId = context.Users.First().Id,
                    StartDate = new DateTime(2020, 4, 4),
                    EndDate = new DateTime(2020, 4, 8),
                    Adults = 2,
                    Kids = 1,
                    ReservationStatusId = context.ReservationStatuses.First().Id,
                    PaymentTypeId = context.PaymentTypes.First().Id,
                    TotalAmount = 1000,
                    AdvancedPayment = 300,
                    ReservationRooms = new List<ReservationRoom> { new ReservationRoom { RoomId = context.Rooms.First().Id } },
                },

                new Reservation()
                {
                    UserId = context.Users.First().Id,
                    StartDate = new DateTime(2020, 5, 4),
                    EndDate = new DateTime(2020, 5, 8),
                    Adults = 2,
                    Kids = 2,
                    ReservationStatusId = context.ReservationStatuses.First().Id,
                    PaymentTypeId = context.PaymentTypes.First().Id,
                    TotalAmount = 1000,
                    AdvancedPayment = 300,
                    ReservationRooms = new List<ReservationRoom> { new ReservationRoom { RoomId = context.Rooms.First().Id } },
                },
                new Reservation()
                {
                    UserId = context.Users.First().Id,
                    StartDate = new DateTime(2020, 6, 4),
                    EndDate = new DateTime(2020, 6, 8),
                    Adults = 1,
                    Kids = 1,
                    ReservationStatusId = context.ReservationStatuses.First().Id,
                    PaymentTypeId = context.PaymentTypes.First().Id,
                    TotalAmount = 1000,
                    AdvancedPayment = 300,
                    ReservationRooms = new List<ReservationRoom> { new ReservationRoom { RoomId = context.Rooms.First().Id } },
                },
            };

            await context.Reservations.AddRangeAsync(reservations);

            await context.SaveChangesAsync();
        }

        public async Task SeedDataForAddAsyncMethodAsync(HotelDbContext context)
        {
            await context.RoomTypes.AddAsync(new RoomType { Name = "TestType" });

            await context.ReservationStatuses.AddAsync(new ReservationStatus { Name = "TestReservationStatus" });

            await context.PaymentTypes.AddAsync(new PaymentType { Name = "TestPaymentType" });

            await context.Hotels.AddAsync(new HotelData
            {
                Name = "Hotel Boryana",
                Address = "Plovdiv",
                PhoneNumber = "123456789",
                UniqueIdentifier = "123456789",
            });

            await context.Users.AddAsync(new HotelUser
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Address = "Address",
                PhoneNumber = "PhoneNumber",
                Email = "email@gmail.com",
            });

            await context.Rooms.AddAsync(new Room
            {
                RoomNumber = "Test-1",
                RoomType = new RoomType { Name = "TestType" },
                Description = "TestDescription",
            });

            await context.SaveChangesAsync();
        }
    }
}
