
using System.ComponentModel.DataAnnotations;

namespace BoardGamePlanner.ViewModels.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            if (value is DateTime date && date > DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(
                ErrorMessage ?? "The game night date must be in the future.");
        }
    }
}
