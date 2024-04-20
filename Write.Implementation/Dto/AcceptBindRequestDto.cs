using System.ComponentModel.DataAnnotations;
using Write.Implementation.Dto.CustomValidation;

namespace Write.Implementation.Dto;

public class AcceptBindRequestDto
{

    [Required, MinLength(4), MaxLength(20)]
    public string BuildingName { get; set; } = string.Empty;
    
    [Required]
    [ValidGuid]
    public string UserToBind { get; set; } = string.Empty;
}
