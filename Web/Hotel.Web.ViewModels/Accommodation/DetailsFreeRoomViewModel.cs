namespace Hotel.Web.ViewModels.Accommodation
{
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class DetailsFreeRoomViewModel : IMapFrom<Room>
    {
        public string Id { get; set; }

        public int RoomNumber { get; set; }

        public string Image { get; set; }

        public string RoomStatusName { get; set; }

        public decimal Price { get; set; }

        public int Capacity { get; set; }

        public string Description { get; set; }

        public string RoomTypeName { get; set; }
    }
}
