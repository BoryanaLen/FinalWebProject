namespace Hotel.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Data.Common.Models;

    public class PaymentType : BaseDeletableModel<string>
    {
        private const int NameMaxLength = 50;

        public PaymentType()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }
    }
}
