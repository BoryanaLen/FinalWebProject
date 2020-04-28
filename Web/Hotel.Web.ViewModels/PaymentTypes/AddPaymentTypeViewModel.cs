namespace Hotel.Web.ViewModels.PaymentTypes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Hotel.Data.Models;
    using Hotel.Services.Mapping;

    public class AddPaymentTypeViewModel : IMapFrom<PaymentType>, IMapTo<Payment>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
