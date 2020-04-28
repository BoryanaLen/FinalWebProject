namespace Hotel.Web.ViewModels.RoomTypes
{
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class DeleteRoomTypeViewModel : IMapFrom<RoomType>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CapacityAdults { get; set; }

        public int CapacityKids { get; set; }

        public string Image { get; set; }

        public IFormFile RoomImage { get; set; }

        public string Description { get; set; }
    }
}
