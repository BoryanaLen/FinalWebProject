namespace Hotel.Web.ViewModels.Payments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class DeletePaymentViewModel : IMapFrom<Payment>
    {
        public string Id { get; set; }

        public DateTime? DateOfPayment { get; set; }

        public decimal Amount { get; set; }

        public string PaymentTypeId { get; set; }

        [Display(Name ="Payment type")]
        public string PaymentTypeName { get; set; }

        public PaymentType PaymentType { get; set; }

        public List<string> ReservationIds { get; set; }
    }
}
