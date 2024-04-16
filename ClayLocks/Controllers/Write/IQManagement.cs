using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Write.Contacts;

namespace ClayLocks.Controllers.Write;

[Route("iq")]
[ApiController]
public class IQManagement: ControllerBase
{
    private readonly ICapPublisher _capPublisher;

    public IQManagement(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher;
    }
    [HttpPost] 
    public async Task<IActionResult> CreateIq()
    {
        await _capPublisher.PublishAsync(nameof(IqCreated), new IqCreated());
        return Ok("added iq");
    }
}