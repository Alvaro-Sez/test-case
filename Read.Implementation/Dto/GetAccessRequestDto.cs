using System.ComponentModel.DataAnnotations;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public class GetAccessRequestDto
{
    [Required] [ValidGuid] public string UserId { get; set; } = string.Empty;

    [ValidGuid] public string UserRequestingId { get; set; } = string.Empty;

    public bool IsAdmin { get; set; } = false;
}