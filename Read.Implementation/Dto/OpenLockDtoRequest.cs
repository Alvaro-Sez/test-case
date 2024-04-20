using System.ComponentModel.DataAnnotations;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public class OpenLockDtoRequest
{
    [ValidGuid][Required]
    public string? LockId { get; set; } 
    
}