using ClayLocks.IDP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Read.Contracts.Queries;
using Read.Implementation.Queries.IQ;
using Write.Contacts.Commands;
using Write.Implementation.Commands.IQ;
using Write.Implementation.Dto;

namespace ClayLocks.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class Iq: ApiController
{
    private readonly ICommandHandler<CreateIQCommand> _handler;
    private readonly IQueryHandler<IEnumerable<GetIqsQuery>> _getIqs;

    public Iq(ICommandHandler<CreateIQCommand> handler,
    IQueryHandler<IEnumerable<GetIqsQuery>> getIqs,
    UserManager<IdentityUser> userManager):base(userManager)
    {
        _handler = handler;
        _getIqs = getIqs;
    }
    [Authorize(Roles=Roles.Admin)]
    [HttpPost] 
    public async Task<IActionResult> Create(CreateIqDto dto)
    {
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