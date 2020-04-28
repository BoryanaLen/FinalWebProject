namespace Hotel.Web.ViewModels.RoomTypes
{
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditRoomTypeViewModel : IMapFrom<RoomType>
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Image { get; set; }

        public IFormFile RoomImage { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue)]
        public int CapacityAdults { get; set; }

        [Range(0, double.MaxValue)]
        public int CapacityKids { get; set; }

        [MinLength(1)]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
