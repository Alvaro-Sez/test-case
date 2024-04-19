using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Read.Contracts.Commands;
using Read.Contracts.Queries;
using Read.Implementation.Command.AccessRequest;
using Read.Implementation.Dto;
using Read.Implementation.Queries.AccessRequest;
using Shared;
using Write.Implementation.Commands.Access;

namespace ClayLocks.Controllers;

[Route("[controller]/[action]")]
[Authorize]
[ApiController]
public class Access : ApiController
{
    private readonly ICommandHandler<CreateHigherAccessRequestCommand> _accessHandler;
    private readonly IQueryHandlerT<GetAccessRequestsQuery, GetAccessRequestDto> _accessRequestQueryHandler;
    private readonly Write.Contacts.Commands.ICommandHandler<AcceptHigherAccessCommand> _acceptHigher;
    private readonly UserManager<IdentityUser> _userManager;
    public Access(ICommandHandler<CreateHigherAccessRequestCommand> accessHandler, UserManager<IdentityUser> userManager, IQueryHandlerT<GetAccessRequestsQuery, GetAccessRequestDto> accessRequestQueryHandler, Write.Contacts.Commands.ICommandHandler<AcceptHigherAccessCommand> acceptHigher)
    {
        _accessHandler = accessHandler;
        _userManager = userManager;
        _accessRequestQueryHandler = accessRequestQueryHandler;
        _acceptHigher = acceptHigher;
    }

    [HttpPost] 
    public async Task<IActionResult> CreateRequest(HigherAccessDto dto)
    {
        var userIdpId = _userManager.GetUserId(User)!;
        var result = await _accessHandler.HandleAsync(new CreateHigherAccessRequestCommand(userIdpId, dto.IqId));
        return ToActionResult(result);
    }
    
    [HttpPost] 
    public async Task<IActionResult> GetRequests(GetAccessRequestDto dto)
    {
        return ToActionResult(await _accessRequestQueryHandler.HandleAsync(dto));
    }
    [HttpPost] 
    public async Task<IActionResult> AcceptRequest()
    {
        var userIdpId = _userManager.GetUserId(User)!;
        var result = await _acceptHigher.HandleAsync(new AcceptHigherAccessCommand{UserRequestingId =Guid.Parse(userIdpId)});
        return ToActionResult(result);
    }
}