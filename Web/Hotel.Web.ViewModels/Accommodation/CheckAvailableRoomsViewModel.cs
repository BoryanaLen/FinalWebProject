namespace Hotel.Web.ViewModels.Accommodation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Web.ViewModels.Attributes.Validation;

    public class CheckAvailableRoomsViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DateGreaterThanCurrenrDateAttribute]
        public DateTime? CheckIn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [EndDateAfterStartDate]
        public DateTime? CheckOut { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than zero for adults")]
        public int Adults { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than or equal to zero for kids")]
        public int Kids { get; set; }
    }
}
