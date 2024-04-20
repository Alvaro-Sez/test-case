using System.ComponentModel.DataAnnotations;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public class HigherAccessDto
{
    [Required] [ValidGuid] public string IqId { get; set; } = string.Empty;
}