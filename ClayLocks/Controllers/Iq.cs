using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Read.Contracts.Queries;
using Shared;
using Write.Contacts.Commands;
using Write.Implementation.Commands.IQ;
using Write.Implementation.Dto;

namespace ClayLocks.Controllers;

[Route("[controller]/[action]")]
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
    [Authorize(Roles="Admin")]
    [HttpPost] 
    public async Task<IActionResult> Create(CreateIqDto dto)
    {//TODO implement validation of the admin
        var result = await _handler.HandleAsync(new CreateIQCommand(dto.BuildingName));
        return ToActionResult(result);
    }
    [Authorize]
    [HttpGet] 
    public async Task<IActionResult> GetAll()
    {
        var iqs = await _getIqs.HandleAsync();
        return ToActionResult(iqs);
    }
}