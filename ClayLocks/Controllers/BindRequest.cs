using Microsoft.AspNetCore.Mvc;

namespace ClayLocks.Controllers;

[Route("bind")]
[ApiController]
public class BindRequest : ApiController
{
    [HttpPost] 
    public Task<IActionResult> CreateBindRequest()
    {
        throw new NotImplementedException();
    }
    [HttpGet] 
    public Task<IActionResult> GetRequests()
    {
        // get requests by the permissions of the client
        throw new NotImplementedException();
    }
    
    [HttpPost] 
    public Task<IActionResult> AcceptRequest()
    {
        throw new NotImplementedException();
    }
}