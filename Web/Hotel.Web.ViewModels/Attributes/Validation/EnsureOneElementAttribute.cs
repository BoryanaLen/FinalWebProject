namespace Hotel.Web.ViewModels.Attributes.Validation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class EnsureOneElementAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as IList;

            if (list.Count == 0)
            {
                return new ValidationResult("At least one check is required");
            }

            return ValidationResult.Success;
        }
    }
}
