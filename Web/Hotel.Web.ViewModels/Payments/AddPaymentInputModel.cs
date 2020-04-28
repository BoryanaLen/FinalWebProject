namespace Hotel.Web.ViewModels.Payments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.Attributes.Validation;
    using Hotel.Web.ViewModels.PaymentTypes;
    using Hotel.Web.ViewModels.Reservations;

    public class AddPaymentInputModel : IMapFrom<Payment>, IMapTo<Payment>
    {
        public AddPaymentInputModel()
        {
            this.ListOfPaymentTypes = new HashSet<PaymentTypeDropDownViewModel>();
        }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of payment")]
        public DateTime? DateOfPayment { get; set; }

        public decimal Amount { get; set; }

        public string PaymentTypeId { get; set; }

        [Display(Name = "Payment type")]
        public PaymentType PaymentType { get; set; }

        [EnsureOneElementAttribute]
        public List<string> ReservationIds { get; set; }

        public IEnumerable<PaymentTypeDropDownViewModel> ListOfPaymentTypes { get; set; }

        public IEnumerable<DetailsReservationViewModel> ListOfNotPaidReservations { get; set; }
    }
}
