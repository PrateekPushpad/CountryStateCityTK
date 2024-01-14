using System.ComponentModel.DataAnnotations;

namespace UI.Validations
{
    public class UppercaseAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string stringValue = value.ToString();
                if (!string.IsNullOrEmpty(stringValue))
                {
                    char Firstletter = stringValue[0];
                    if (char.IsUpper(Firstletter))
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            return new ValidationResult(ErrorMessage ?? "The First letter must be Uppercase");
        }
    }
}
