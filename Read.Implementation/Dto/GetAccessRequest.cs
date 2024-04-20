using System.ComponentModel.DataAnnotations;
using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Dto;

public class GetAccessRequest
{
    public GetAccessRequest(Guid userId, bool isAdmin)
    {
        UserId = userId;
        IsAdmin = isAdmin;
    }
    public Guid UserId { get; set; }  
    public bool IsAdmin { get; set; } = false;
}