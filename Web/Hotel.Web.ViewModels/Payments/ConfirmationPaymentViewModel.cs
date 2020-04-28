namespace Hotel.Web.ViewModels.Payments
{
    using System;
    using System.Collections.Generic;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class ConfirmationPaymentViewModel : IMapFrom<Payment>
    {
        public IEnumerable<DetailsPaymentViewModel> Payments;
    }
}
