namespace Hotel.Web.ViewModels.Accommodation
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Hotel.Web.ViewModels.RoomTypes;

    public class IndexViewModel
    {
        public IEnumerable<DetailsRoomTypeViewModel> RoomTypes { get; set; }

        public CheckAvailableRoomsViewModel CheckAvailableRoomsViewModel { get; set; }
    }
}
