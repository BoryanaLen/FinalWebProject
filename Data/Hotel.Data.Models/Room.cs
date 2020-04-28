namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Common.Models;

    public class Room : BaseDeletableModel<string>
    {
        private const int RoomNumberMaxLength = 20;
        private const int DescriptionMaxLength = 1400;

        public Room()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(RoomNumberMaxLength)]
        public string RoomNumber { get; set; }

        [Required]
        public string RoomTypeId { get; set; }

        public virtual RoomType RoomType { get; set; }


        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public string HotelDataId { get; set; }

        public virtual HotelData HotelData { get; set; }

        public virtual ICollection<ReservationRoom> ReservationRooms { get; set; }
    }
}
