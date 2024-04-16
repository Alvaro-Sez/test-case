using Microsoft.AspNetCore.Mvc;

namespace ClayLocks.Controllers;

[Route("permission")]
[ApiController]
public class Permissions : ApiController 
{
    
    [HttpPost] 
    public Task<IActionResult> RequestLockPermission()
    {
        throw new NotImplementedException();
    }
    [HttpPost] 
    public Task<IActionResult> RequestHigherPermission()
    {
        throw new NotImplementedException();
    }
    [HttpPost] 
    public Task<IActionResult> GetPermissionRequests()
    {
        throw new NotImplementedException();
    }
    [HttpPost] 
    public Task<IActionResult> ManagePermissions()
    {
        throw new NotImplementedException();
    }
}