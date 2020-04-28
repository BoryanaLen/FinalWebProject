namespace Hotel.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Common.Models;

    public class SpecialOffer : BaseDeletableModel<string>
    {
        private const int TitleMaxLength = 50;
        private const int ContentMaxLength = 1500;
        private const int ShortContentMaxLength = 50;

        public SpecialOffer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        [Required]
        [MaxLength(ShortContentMaxLength)]
        [DataType(DataType.Html)]
        public string ShortContent { get; set; }

        public string HotelDataId { get; set; }

        public virtual HotelData HotelData { get; set; }
    }
}
