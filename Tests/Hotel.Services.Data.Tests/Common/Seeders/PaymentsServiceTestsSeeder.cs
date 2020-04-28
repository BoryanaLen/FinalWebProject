namespace Hotel.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Hotel.Data;
    using Hotel.Data.Models;

    public class PaymentsServiceTestsSeeder
    {
        public async Task SeedPaymentAsync(HotelDbContext context)
        {
            await this.SeedDataAsync(context);

            var payment = new Payment
            {
                DateOfPayment = DateTime.Now.Date,
                Amount = 300,
                PaymentTypeId = context.PaymentTypes.First().Id,
                ReservationPayments = new List<ReservationPayment>
                        { new ReservationPayment { ReservationId = context.Reservations.First().Id } },
            };

            await context.Payments.AddAsync(payment);

            await context.SaveChangesAsync();
        }

        public async Task SeedPaymentsAsync(HotelDbContext context)
        {
            await this.SeedDataAsync(context);

            var payments = new List<Payment>()
            {
                new Payment()
                {
                    DateOfPayment = DateTime.Now,
                    Amount = 300,
                    PaymentTypeId = context.PaymentTypes.First().Id,
                    ReservationPayments = new List<ReservationPayment>
                        { new ReservationPayment { ReservationId = context.Reservations.First().Id } },
                },
                new Payment()
                {
                    DateOfPayment = DateTime.Now.AddDays(-1),
                    Amount = 200,
                    PaymentTypeId = context.PaymentTypes.First().Id,
                    ReservationPayments = new List<ReservationPayment>
                        { new ReservationPayment { ReservationId = context.Reservations.First().Id } },
                },
                new Payment()
                {
                    DateOfPayment = DateTime.Now.AddDays(-2),
                    Amount = 100,
                    PaymentTypeId = context.PaymentTypes.First().Id,
                    ReservationPayments = new List<ReservationPayment>
                        { new ReservationPayment { ReservationId = context.Reservations.First().Id } },
                },
            };

            await context.Payments.AddRangeAsync(payments);

            await context.SaveChangesAsync();
        }

        public async Task SeedDataAsync(HotelDbContext context)
        {
            await context.PaymentTypes.AddAsync(new PaymentType() { Name = "TestType" });

            await context.Users.AddAsync(new HotelUser
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Address = "Address",
                PhoneNumber = "PhoneNumber",
                Email = "email@gmail.com",
            });

            await context.SaveChangesAsync();

            var reservation = new Reservation
            {
                UserId = context.Users.First().Id,
                StartDate = new DateTime(2020, 4, 4),
                EndDate = new DateTime(2020, 4, 8),
                Adults = 2,
                Kids = 1,
            };

            await context.Reservations.AddAsync(reservation);

            await context.SaveChangesAsync();
        }
    }
}
