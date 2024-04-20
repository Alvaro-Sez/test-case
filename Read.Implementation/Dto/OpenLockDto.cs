using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public class OpenLockDto
{
    public OpenLockDto(Guid userId, string lockId)
    {
        UserId = userId;
        LockId = Guid.Parse(lockId);
    }
    public Guid UserId { get; set; }
    public Guid LockId { get; set; } 
}