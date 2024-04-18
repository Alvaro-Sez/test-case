using System.ComponentModel.DataAnnotations;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public record UserIdDto
{
    public UserIdDto(string userId)
    {
        if(!Guid.TryParse(userId, out var id))
        {
            throw new ArgumentException("Invalid Id from Idp service");
        }
        UserId = id;
    }
    public Guid UserId{ get; init; }
}