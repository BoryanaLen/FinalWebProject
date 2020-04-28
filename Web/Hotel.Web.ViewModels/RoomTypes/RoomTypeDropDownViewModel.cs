namespace Hotel.Web.ViewModels.RoomTypes
{
    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class RoomTypeDropDownViewModel : IMapFrom<RoomType>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
