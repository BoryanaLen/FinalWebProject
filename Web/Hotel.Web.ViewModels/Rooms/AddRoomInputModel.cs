namespace Hotel.Web.ViewModels.Rooms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.RoomTypes;
    using Microsoft.AspNetCore.Http;

    public class AddRoomInputModel : IMapFrom<Room>, IMapTo<Room>
    {
        public AddRoomInputModel()
        {
            this.ListOfRoomTypes = new HashSet<RoomTypeDropDownViewModel>();
        }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string RoomNumber { get; set; }

        [Required]
        public string RoomTypeId { get; set; }

        [MinLength(1)]
        [MaxLength(100)]
        public string Description { get; set; }

        public string HotelDataId { get; set; }

        public IEnumerable<RoomTypeDropDownViewModel> ListOfRoomTypes { get; set; }
    }
}
