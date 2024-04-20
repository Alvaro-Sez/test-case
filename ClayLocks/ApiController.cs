using System.Security.Claims;
using ClayLocks.IDP;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace ClayLocks;

[ApiController]
public class ApiController : ControllerBase
{
   protected readonly UserManager<IdentityUser> UserManager;
   protected ApiController(UserManager<IdentityUser> userManager)
   {
      UserManager = userManager;
   }
   protected IActionResult ToActionResult(Result result)
   {
      if (!result.IsSuccess)
      {
         return IsDevelopment() ? BadRequest(result.Error) : BadRequest(result.Error.Code);
      }
      return Ok();
   }
   
   protected IActionResult ToActionResult<T>(Result<T> result)
   {
      if (!result.IsSuccess)
      {
         return IsDevelopment() ? BadRequest(result.Error) : BadRequest(result.Error.Code);
      }
      if(result.Value is null)
      {
         return NoContent();
      }
      return Ok(result.Value);
   }
   
    protected static bool IsAdmin(ClaimsPrincipal user)
    {
        var userIdentity = (ClaimsIdentity)user.Identity!;
        var claims = userIdentity.Claims;
        var roleClaimType = userIdentity.RoleClaimType;
        var claimRoles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
        var roles =claimRoles.Select(c => c.Value);
        return roles.Contains(Roles.Admin);
    } 
   protected Guid GetUserId(ClaimsPrincipal user)
   {
      var userId =UserManager.GetUserId(user)!;
      if(!Guid.TryParse(userId, out var id))//protection against idp needed?
      {
         throw new ArgumentException("Invalid Id from Idp service");
      }
      return id;
   }
   private static bool IsDevelopment() => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
}
