using System.ComponentModel.DataAnnotations;
using Write.Contacts.Dto.CustomValidation;

namespace Write.Contacts.Dto;

public class AcceptRequestDto
{
    public AcceptRequestDto(string buildingName)
    {
        BuildingName = buildingName;
    }
    
    [Required, MinLength(4), MaxLength(20)]
    public string BuildingName { get; set; }
    
    [Required]
    [ValidGuid]
    public string UserToBind { get; set; }
}