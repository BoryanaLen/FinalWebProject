namespace Hotel.Web.ViewModels.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllReservationsViewModel : PagedListViewModel
    {
        public IEnumerable<DetailsReservationViewModel> Reservations { get; set; }
    }
}
