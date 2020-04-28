namespace Hotel.Web.ViewModels.PaymentTypes
{
    using System.Collections.Generic;

    public class AllPaymentTypesViewModel : PagedListViewModel
    {
        public IEnumerable<DetailsPaymentTypeViewModel> RoomTypes { get; set; }
    }
}
