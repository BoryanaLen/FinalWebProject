namespace Hotel.Web.ViewModels.Rooms
{
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class AvailableRoomViewModel : IMapFrom<Room>, IMapFrom<RoomType>
    {
        public string Id { get; set; }

        public string RoomNumber { get; set; }

        public string RoomRoomTypeId { get; set; }

        public string RoomRoomTypeName { get; set; }

        public string RoomRoomTypeImage { get; set; }

        public decimal RoomRoomTypePrice { get; set; }

        public int RoomRoomTypeCapacityAdults { get; set; }

        public int RoomRoomTypeCapacityKids { get; set; }

        public string Description { get; set; }

        public bool IsChecked { get; set; }
    }
}
