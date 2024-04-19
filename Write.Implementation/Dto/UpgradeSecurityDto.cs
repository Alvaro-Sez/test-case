using System.ComponentModel.DataAnnotations;
using Write.Implementation.Dto.CustomValidation;

namespace Write.Implementation.Dto;

public class UpgradeSecurityDto
{
    [Required]
    [ValidGuid]
    public string LockId { get; set; } = string.Empty;
}