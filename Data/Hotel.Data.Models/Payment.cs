namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Common.Models;

    public class Payment : BaseDeletableModel<string>
    {
        public Payment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ReservationPayments = new HashSet<ReservationPayment>();
        }

        [Required]
        public DateTime DateOfPayment { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentTypeId { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        public virtual ICollection<ReservationPayment> ReservationPayments { get; set; }
    }
}
