using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Write.Contacts.Commands;
using Write.Contacts.Dto;
using Write.Implementation.Commands.IQ;

namespace ClayLocks.Controllers;

[Route("iq")]
[ApiController]
public class Iq: ApiController
{
    private readonly ICapPublisher _capPublisher;
    private readonly ICommandHandler<CreateIQCommand> _handler;

    public Iq(ICapPublisher capPublisher, ICommandHandler<CreateIQCommand> handler)
    {
        _capPublisher = capPublisher;
        _handler = handler;
    }
    [HttpPost] 
    public async Task<IActionResult> CreateIq(CreateIqDto dto)
    {
        // mapping
        await _handler.HandleAsync(new CreateIQCommand());
        //validation
        return Ok("added iq");
    }
    
    [HttpGet] 
    public Task<IActionResult> GetIqs()
    {
        throw new NotImplementedException();
    }
}