namespace Hotel.Web.ViewModels.Attributes.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Hotel.Web.ViewModels.Accommodation;

    public class EndDateAfterStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (CheckAvailableRoomsViewModel)validationContext.ObjectInstance;

            DateTime checkIn = Convert.ToDateTime(model.CheckIn);

            if ((DateTime)value <= checkIn)
            {
                return new ValidationResult("CheckOut date should be greater than CheckIn date!");
            }

            return ValidationResult.Success;
        }
    }
}
