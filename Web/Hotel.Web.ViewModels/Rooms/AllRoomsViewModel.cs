namespace Hotel.Web.ViewModels.Rooms
{
    using System.Collections.Generic;

    public class AllRoomsViewModel : PagedListViewModel
    {
        public IEnumerable<DetailsRoomViewModel> Rooms { get; set; }
    }
}
