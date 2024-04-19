using System.ComponentModel.DataAnnotations;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public class OpenLockDto
{
    public string? UserId { get; set; }
    [ValidGuid] [Required] public string LockId { get; set; } = string.Empty;
}