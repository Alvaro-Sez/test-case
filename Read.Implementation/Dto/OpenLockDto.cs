using System.ComponentModel.DataAnnotations;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public class OpenLockDto
{
    [ValidGuid] [Required] public string UserId { get; set; } = string.Empty;
    [ValidGuid] [Required] public string LockId { get; set; } = string.Empty;
}