namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Hotel.Data.Common.Models;

    public class Reservation : BaseDeletableModel<string>
    {
        public Reservation()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ReservationPayments = new HashSet<ReservationPayment>();
            this.ReservationRooms = new HashSet<ReservationRoom>();
        }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int Adults { get; set; }

        [Required]
        public int Kids { get; set; }

        [Required]
        [ForeignKey("HotelUser")]
        public string UserId { get; set; }

        public virtual HotelUser User { get; set; }

        [Required]
        public string ReservationStatusId { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }

        [Required]
        public string PaymentTypeId { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        public decimal PricePerDay { get; set; }

        public int TotalDays { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal AdvancedPayment { get; set; }

        public decimal AmountForPayment => this.TotalAmount - this.AdvancedPayment;

        [Required]
        public bool IsPaid => false;

        public virtual ICollection<ReservationRoom> ReservationRooms { get; set; }

        public virtual ICollection<ReservationPayment> ReservationPayments { get; set; }
    }
}
