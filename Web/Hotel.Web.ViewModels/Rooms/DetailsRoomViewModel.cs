namespace Hotel.Web.ViewModels.Rooms
{
    using AutoMapper;
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class DetailsRoomViewModel : IMapFrom<Room>
    {
        public string Id { get; set; }

        public string RoomNumber { get; set; }

        public string RoomTypeImage { get; set; }

        public decimal RoomTypePrice { get; set; }

        public int RoomTypeCapacityAdults { get; set; }

        public int RoomTypeCapacityKids { get; set; }

        public string Description { get; set; }

        public string RoomTypeName { get; set; }
    }
}
