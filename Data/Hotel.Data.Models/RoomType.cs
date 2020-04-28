namespace Hotel.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Common.Models;

    public class RoomType : BaseDeletableModel<string>
    {
        private const int NameMaxLength = 50;
        private const int DescriptionMaxLength = 1400;

        public RoomType()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int CapacityAdults { get; set; }

        public int CapacityKids { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
