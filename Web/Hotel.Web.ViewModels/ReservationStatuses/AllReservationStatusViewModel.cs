namespace Hotel.Web.ViewModels.ReservationStatuses
{
    using System.Collections.Generic;

    public class AllReservationStatusViewModel : PagedListViewModel
    {
        public IEnumerable<DetailsReservationStatusViewModel> RoomTypes { get; set; }
    }
}
