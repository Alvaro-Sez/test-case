using System.ComponentModel.DataAnnotations;

namespace Write.Implementation.Dto;

public class CreateIqDto(string buildingName)
{
    [Required, MinLength(4), MaxLength(20)]
    public string BuildingName { get; init; } = buildingName;
}