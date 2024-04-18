using System.ComponentModel.DataAnnotations;

namespace Write.Implementation.Dto.CustomValidation;

public class ValidGuidAttribute : ValidationAttribute
{
    public ValidGuidAttribute(): base("The field {0} must be a valid GUID")
    {

    }
    public override bool IsValid(object value)
    {
        return Guid.TryParse(value.ToString(), out var guid);
    }
}