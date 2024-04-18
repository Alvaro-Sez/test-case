using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClayLocks.Controllers;

[Route("admin")]
[ApiController]
public class Admin : ApiController
{
    
   private readonly RoleManager<IdentityRole> _roleManager;
   private readonly UserManager<IdentityUser> _userManager;

   public Admin(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
   {
      _roleManager = roleManager;
      _userManager = userManager;
   }
   [Authorize]
   [HttpGet] 
   public async Task<IActionResult> CreateAdmin()
   {
      var roles = new[]{"admin", "high"};
      foreach (var role in roles)
      {
         if(!(await _roleManager.RoleExistsAsync(role)))
         {
            var entityRole = new IdentityRole("admin");
            await _roleManager.CreateAsync(entityRole);
         }
      }
      var user = await _userManager.GetUserAsync(User);
      await _userManager.AddToRoleAsync(user, "admin");
      user = await _userManager.GetUserAsync(User);
      
      return Ok(user);
   }
}