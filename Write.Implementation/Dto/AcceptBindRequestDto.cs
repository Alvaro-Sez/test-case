using System.ComponentModel.DataAnnotations;
using Write.Implementation.Dto.CustomValidation;

namespace Write.Implementation.Dto;

public class AcceptBindRequestDto
{
    public AcceptBindRequestDto(string buildingName)
    {
        BuildingName = buildingName;
    }
    
    [Required, MinLength(4), MaxLength(20)]
    public string BuildingName { get; set; }
    
    [Required]
    [ValidGuid]
    public string UserToBind { get; set; }
}
