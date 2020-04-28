namespace Hotel.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IReservationPaymentsService
    {
        IEnumerable<string> GetAllReservationsByPaymentId(string id);

        IEnumerable<string> GetAllPaymentsByReservationId(string id);
    }
}
