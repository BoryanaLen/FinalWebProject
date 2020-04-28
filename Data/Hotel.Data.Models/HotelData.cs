namespace Hotel.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Common.Models;

    public class HotelData : BaseDeletableModel<string>
    {

        private const int HotelDataNameMaxLength = 100;
        private const int UniqueIdentifierMaxLength = 20;
        private const int AddressMaxLength = 200;

        public HotelData()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<HotelUser>();
        }

        [Required]
        [MaxLength(HotelDataNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(UniqueIdentifierMaxLength)]
        public string UniqueIdentifier { get; set; }

        public string Manager { get; set; }

        public string Owner { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public virtual IEnumerable<HotelUser> Users { get; set; }
    }
}
