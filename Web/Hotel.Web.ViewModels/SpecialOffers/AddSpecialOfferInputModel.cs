namespace Hotel.Web.ViewModels.SpecialOffers
{
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class AddSpecialOfferInputModel : IMapFrom<SpecialOffer>
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Html)]
        public string ShortContent { get; set; }

        public string HotelDataId { get; set; }
    }
}
