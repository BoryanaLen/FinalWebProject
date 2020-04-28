namespace Hotel.Web.ViewModels.Payments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllPaymentsViewModel : PagedListViewModel
    {
        public IEnumerable<DetailsPaymentViewModel> Payments { get; set; }
    }
}
