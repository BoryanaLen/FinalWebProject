namespace Hotel.Web.ViewModels.RoomTypes
{
    using System.Collections.Generic;

    public class AllRoomTypesViewModel : PagedListViewModel
    {
        public IEnumerable<DetailsRoomTypeViewModel> RoomTypes { get; set; }
    }
}
