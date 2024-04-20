using ClayLocks.IDP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClayLocks.Controllers;

// ONLY FOR TEST
[Route("admin")]
[ApiController]
public class Admin : ApiController
{
    
// ONLY FOR TEST
   private readonly RoleManager<IdentityRole> _roleManager;
   private readonly UserManager<IdentityUser> _userManager;

   public Admin(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
   {
      _roleManager = roleManager;
      _userManager = userManager;
   }
// ONLY FOR TEST
   [Authorize]
   [HttpGet] 
   public async Task<IActionResult> CreateAdmin()
   {
      var roles = new[]{Roles.Admin};
      foreach (var role in roles)
      {
         if(!(await _roleManager.RoleExistsAsync(role)))
         {
            var entityRole = new IdentityRole(role);
            await _roleManager.CreateAsync(entityRole);
         }
      }
      var user = await _userManager.GetUserAsync(User);
      await _userManager.AddToRoleAsync(user!, Roles.Admin);
      user = await _userManager.GetUserAsync(User);
      
      return Ok(user);
   }
}