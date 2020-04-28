namespace Hotel.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Hotel.Data.Models;
    using Hotel.Services.Data;
    using Microsoft.Extensions.DependencyInjection;

    public class PaymentSeeder : ISeeder
    {
        public async Task SeedAsync(HotelDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Payments.Any())
            {
                return;
            }

            var paymentsService = serviceProvider.GetRequiredService<IPaymentsService>();

            var paymentTypesService = serviceProvider.GetRequiredService<IPaymentTypesService>();
            var reservationsService = serviceProvider.GetRequiredService<IReservationsService>();
            PaymentType paymentType = await paymentTypesService.GetPaymentTypeByNameAsync("Direct bank transfer");
            Reservation reservationOne = reservationsService.GetAllReservationsList().First();
            Reservation reservationTwo = reservationsService.GetAllReservationsList().Last();

            var paymentOne = new Payment
            {
                DateOfPayment = DateTime.UtcNow.AddDays(-3),
                Amount = 300,
                PaymentTypeId = paymentType.Id,
            };

            var reservationPaymentOne = new ReservationPayment
            {
                PaymentId = paymentOne.Id,
                ReservationId = reservationOne.Id,
            };

            paymentOne.ReservationPayments = new List<ReservationPayment> { reservationPaymentOne };
            await paymentsService.AddPaymentAsync(paymentOne);

            var paymentTwo = new Payment
            {
                DateOfPayment = DateTime.UtcNow.AddDays(-2),
                Amount = 500,
                PaymentTypeId = paymentType.Id,
            };

            var reservationPaymentTwo = new ReservationPayment
            {
                PaymentId = paymentTwo.Id,
                ReservationId = reservationTwo.Id,
            };

            paymentTwo.ReservationPayments = new List<ReservationPayment> {reservationPaymentTwo };
            await paymentsService.AddPaymentAsync(paymentTwo);

            reservationOne.AdvancedPayment = paymentOne.Amount;
            reservationTwo.AdvancedPayment = paymentTwo.Amount;
            await dbContext.SaveChangesAsync();
        }
    }
}
