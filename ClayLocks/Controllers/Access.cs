using Microsoft.AspNetCore.Mvc;

namespace ClayLocks.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class Access : ApiController 
{
    
    [HttpGet] 
    public Task<IActionResult> GetLocks()
    {
        throw new NotImplementedException();
    }
    // [HttpPost] 
    // public Task<IActionResult> RequestHigherPermission()
    // {
    //     throw new NotImplementedException();
    // }
    // [HttpPost] 
    // public Task<IActionResult> GetPermissionRequests()
    // {
    //     throw new NotImplementedException();
    // }
    // [HttpPost] 
    // public Task<IActionResult> ManagePermissions()
    // {
    //     throw new NotImplementedException();
    // }
}