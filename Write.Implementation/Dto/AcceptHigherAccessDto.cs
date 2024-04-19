using System.ComponentModel.DataAnnotations;
using Write.Implementation.Dto.CustomValidation;

namespace Write.Implementation.Dto;

public class AcceptHigherAccessDto
{
    [Required] [ValidGuid] public string UserId { get; set; } = string.Empty;
}