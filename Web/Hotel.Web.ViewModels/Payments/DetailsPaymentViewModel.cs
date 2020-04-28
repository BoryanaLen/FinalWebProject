namespace Hotel.Web.ViewModels.Payments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.PaymentTypes;

    public class DetailsPaymentViewModel : IMapFrom<Payment>
    {
        public DetailsPaymentViewModel()
        {
            this.ReservationIds = new List<string>();
        }

        public string Id { get; set; }

        public DateTime DateOfPayment { get; set; }

        public decimal Amount { get; set; }

        public string PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }

        public string PaymentTypeName { get; set; }

        public string ReservationNumbers { get; set; }

        public List<string> ReservationIds { get; set; }
    }
}
