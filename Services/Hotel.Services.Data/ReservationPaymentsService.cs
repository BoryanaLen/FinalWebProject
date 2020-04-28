namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Hotel.Data.Common.Repositories;
    using Hotel.Data.Models;

    public class ReservationPaymentsService : IReservationPaymentsService
    {
        private readonly IRepository<ReservationPayment> reservationPaymentsRepository;

        public ReservationPaymentsService(IRepository<ReservationPayment> reservationPaymentsRepository)
        {
            this.reservationPaymentsRepository = reservationPaymentsRepository;
        }

        public IEnumerable<string> GetAllPaymentsByReservationId(string id)
        {
            var paymentsIds = this.reservationPaymentsRepository
                .AllAsNoTracking()
                .Where(x => x.ReservationId == id)
                .Select(x => x.PaymentId)
                .ToList();

            return paymentsIds;
        }

        public IEnumerable<string> GetAllReservationsByPaymentId(string id)
        {
            var reservationIds = this.reservationPaymentsRepository
                .AllAsNoTracking()
                .Where(x => x.PaymentId == id)
                .Select(x => x.ReservationId)
                .ToList();

            return reservationIds;
        }
    }
}
