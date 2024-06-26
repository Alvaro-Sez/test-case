using System.ComponentModel.DataAnnotations;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public record UserIdDto
{
    public UserIdDto(Guid userId)
    {
        UserId = userId;
    }
    public Guid UserId{ get; init; }
}