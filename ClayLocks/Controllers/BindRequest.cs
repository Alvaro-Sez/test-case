using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Read.Contracts.Entities;
using Read.Contracts.Queries;
using Read.Implementation.Command.BindRequest;
using Read.Implementation.Queries.BindRequest;
using Write.Contacts.Commands;
using Write.Implementation.Commands.BindRequests;
using Write.Implementation.Dto;

namespace ClayLocks.Controllers;

[Route("[controller]/[action]")]
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
    
    [Authorize]
    [HttpPost] //TODO protected
    public async Task<IActionResult> Add(BindRequestDto dto)
    {
        var userIdpId = _userManager.GetUserId(User)!;
        var result = await _createBindRequestHandler
            .HandleAsync(new CreateBindRequestCommand(dto.BuildingName,userIdpId));
        return ToActionResult(result);
    }
    
    // [Authorize(Roles="Admin")]
    [HttpGet] 
    public async Task<IActionResult> GetAll()
    {
        var result = await _queryHandler.HandleAsync();
        return ToActionResult(result); 
    }
    
    // [Authorize(Roles="Admin")]
    [HttpPost] 
    public async Task<IActionResult> Accept(AcceptRequestDto dto)
    {
        var userIdpId = _userManager.GetUserId(User)!;
        var result = await _acceptBindRequestHandler.HandleAsync(new AcceptBindRequestCommand(dto.BuildingName, dto.UserToBind, userIdpId));
        return ToActionResult(result);
    }
}