namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReservationRoom
    {
        public string ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }

        public string RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}
