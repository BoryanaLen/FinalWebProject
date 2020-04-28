namespace Hotel.Web.ViewModels.Payments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Models;

    using Hotel.Services.Mapping;

    public class EditPaymentViewModel : IMapFrom<Payment>
    {
        public string Id { get; set; }

        [Required]
        public DateTime DateOfPayment { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentTypeId { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        public ICollection<ReservationPayment> ReservationPayments { get; set; }
    }
}
