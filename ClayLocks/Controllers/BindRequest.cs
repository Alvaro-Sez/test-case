using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Read.Contracts.Entities;
using Read.Contracts.Queries;
using Read.Implementation.Command.BindRequest;
using Read.Implementation.Queries.BindRequest;
using Write.Contacts.Commands;
using Write.Contacts.Dto;
using Write.Implementation.Commands.BindRequests;

namespace ClayLocks.Controllers;

[Route("bind")]
[ApiController]
public class BindRequest : ApiController
{
    private readonly IQueryHandler<IEnumerable<BindIqRequest>> _queryHandler;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly Read.Contracts.Commands.ICommandHandler<CreateBindRequestCommand> _createBindRequestHandler;
    private readonly ICommandHandler<AcceptBindRequestCommand> _acceptBindRequestHandler;

    public BindRequest(
        IQueryHandler<IEnumerable<BindIqRequest>> queryHandler,
        UserManager<IdentityUser> userManager,
        Read.Contracts.Commands.ICommandHandler<CreateBindRequestCommand> createBindRequestHandler,
        ICommandHandler<AcceptBindRequestCommand> acceptBindRequestHandler)
        
    {
        _queryHandler = queryHandler;
        _userManager = userManager;
        _createBindRequestHandler = createBindRequestHandler;
        _acceptBindRequestHandler = acceptBindRequestHandler;
    }
    [HttpPost] //TODO protected
    public async Task<IActionResult> CreateBindRequest(BindRequestDto dto)
    {
        var userIdpId = _userManager.GetUserId(User)!;
        var result = await _createBindRequestHandler
            .HandleAsync(new CreateBindRequestCommand(dto.BuildingName,userIdpId));
        return ToActionResult(result);
    }
    [Authorize(Roles="Admin")]
    [HttpGet] 
    public async Task<IActionResult> GetIqBindRequests()
    {
        var userIdpId = _userManager.GetUserId(User)!;
        
        var query = new GetBindIqRequestsQuery(userIdpId);
        
        var result = await _queryHandler.HandleAsync();
        return ToActionResult(result); 
    }
    [Authorize(Roles="Admin")]
    [HttpPost("bind/accept")] 
    public async Task<IActionResult> AcceptRequest(AcceptRequestDto dto)
    {
        var userIdpId = _userManager.GetUserId(User)!;
        var result = await _acceptBindRequestHandler.HandleAsync(new AcceptBindRequestCommand(dto.BuildingName, dto.UserToBind, userIdpId));
        return ToActionResult(result);
    }
}