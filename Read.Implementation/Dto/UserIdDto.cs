using System.ComponentModel.DataAnnotations;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public record UserIdDto
{
    public UserIdDto(string userId)
    {
        user
        UserId = Guid.TryParse(userId);
    }
    public Guid UserId{ get; init; }
}