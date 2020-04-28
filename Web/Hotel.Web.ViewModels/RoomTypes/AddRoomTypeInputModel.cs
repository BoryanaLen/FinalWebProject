namespace Hotel.Web.ViewModels.RoomTypes
{
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class AddRoomTypeInputModel : IMapFrom<RoomType>, IMapTo<RoomType>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Capacity Adults")]
        public int CapacityAdults { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Capacity Kids")]
        public int CapacityKids { get; set; }

        public string Image { get; set; }

        [Required]
        public IFormFile RoomImage { get; set; }

        [MaxLength(1400)]
        public string Description { get; set; }
    }
}
