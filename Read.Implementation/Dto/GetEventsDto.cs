using System.ComponentModel.DataAnnotations;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public class GetEventsDto
{
    public string UserId { get; set; } = string.Empty;
}