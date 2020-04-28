namespace Hotel.Web.ViewModels.Rooms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;
    using Hotel.Web.ViewModels.RoomTypes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class DeleteRoomViewModel : IMapFrom<Room>
    {
        public string Id { get; set; }

        [Display(Name = "Room number")]
        public string RoomNumber { get; set; }

        [Display(Name = "Room type")]
        public string RoomTypeId { get; set; }

        public string RoomTypeName { get; set; }

        public string Description { get; set; }
    }
}
