using Microsoft.AspNetCore.Mvc;
using Read.Contracts.Queries;
using Shared;
using Write.Contacts.Commands;
using Write.Contacts.Dto;
using Write.Implementation.Commands.IQ;

namespace ClayLocks.Controllers;

[Route("iq")]
[ApiController]
public class Iq: ApiController
{
    private readonly ICommandHandler<CreateIQCommand> _handler;
    private readonly IQueryHandler<IEnumerable<string>> _getIqs;

    public Iq(ICommandHandler<CreateIQCommand> handler, IQueryHandler<IEnumerable<string>> getIqs)
    {
        _handler = handler;
        _getIqs = getIqs;
    }
    [HttpPost] 
    public async Task<IActionResult> CreateIq(CreateIqDto dto)
    {
        //TODO to test
        var result = await _handler.HandleAsync(new CreateIQCommand(dto.BuildingName));
        return ToActionResult(result);
    }
    
    [HttpGet] 
    public async Task<IActionResult> GetIqs()
    {
        var iqs = await _getIqs.HandleAsync();
        return ToActionResult(iqs);
    }
}