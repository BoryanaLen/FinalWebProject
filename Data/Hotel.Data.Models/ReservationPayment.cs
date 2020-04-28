namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReservationPayment 
    {
        public string ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }

        public string PaymentId { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
