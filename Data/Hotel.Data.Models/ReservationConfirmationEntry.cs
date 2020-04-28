namespace Hotel.Data.Models
{
    using System;

    public class ReservationConfirmationEntry
    {
        public ReservationConfirmationEntry()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
