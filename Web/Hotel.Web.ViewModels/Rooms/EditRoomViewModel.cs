namespace Hotel.Web.ViewModels.Rooms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.RoomTypes;

    public class EditRoomViewModel : IMapFrom<Room>
    {
        public EditRoomViewModel()
        {
            this.ListOfRoomTypes = new HashSet<RoomTypeDropDownViewModel>();
        }

        public string Id { get; set; }

        [MinLength(1)]
        [MaxLength(20)]
        [Display(Name = "Room number")]
        public string RoomNumber { get; set; }

        [Display(Name = "Room type")]
        public string RoomTypeId { get; set; }

        [MinLength(1)]
        [MaxLength(100)]
        public string Description { get; set; }

        public IEnumerable<RoomTypeDropDownViewModel> ListOfRoomTypes { get; set; }
    }
}
