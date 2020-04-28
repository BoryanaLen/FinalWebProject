namespace Hotel.Web.ViewModels.Attributes.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateGreaterThanCurrenrDateAttribute : ValidationAttribute
    {
        private readonly DateTime today;

        public DateGreaterThanCurrenrDateAttribute()
        {
            this.today = DateTime.Today;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value < this.today)
            {
                return new ValidationResult("CheckIn date should be greater or equal to current date!");
            }

            return ValidationResult.Success;
        }
    }
}
