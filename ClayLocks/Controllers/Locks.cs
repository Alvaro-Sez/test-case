using Microsoft.AspNetCore.Mvc;

namespace ClayLocks.Controllers;

[Route("lock")]
[ApiController]
public class Locks : ApiController
{
    
    [HttpGet] 
    public Task<IActionResult> OpenLock()
    {
        throw new NotImplementedException();
    }
}