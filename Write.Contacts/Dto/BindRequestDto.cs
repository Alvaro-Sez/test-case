using System.ComponentModel.DataAnnotations;

namespace Write.Contacts.Dto;

public class BindRequestDto(string buildingName)
{
    [Required, MinLength(4), MaxLength(20)]
    public string BuildingName { get; set; } = buildingName;

    [Required]
    public Guid IdpId{ get; set; }
}