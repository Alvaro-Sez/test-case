using System.ComponentModel.DataAnnotations;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public class UserDto
{
    [ValidGuid]
    [Required]
    public string UserId{ get; set; }
}