using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Read.Contracts.Entities;
using Read.Implementation.Command.BindRequest;
using Read.Implementation.Queries.BindRequest;
using Write.Contacts.Commands;
using Write.Contacts.Dto;
using Write.Contacts.Queries;
using Write.Implementation.Commands.BindRequests;

namespace ClayLocks.Controllers;

[Route("bind")]
[ApiController]
public class BindRequest : ApiController
{
    private readonly IQueryHandler<GetBindIqRequestsQuery,IEnumerable<BindIqRequest>> _queryHandler;
   private readonly UserManager<IdentityUser> _userManager;
   private readonly ICommandHandler<CreateBindRequestCommand> _createBindRequestHandler;
   private readonly ICommandHandler<AcceptBindRequestCommand> _acceptBindRequestHandler;

   public BindRequest(
        IQueryHandler<GetBindIqRequestsQuery,IEnumerable<BindIqRequest>> queryHandler,
        UserManager<IdentityUser> userManager,
        ICommandHandler<CreateBindRequestCommand> createBindRequestHandler,
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
        var result = await _createBindRequestHandler.HandleAsync(new CreateBindRequestCommand(dto.BuildingName,userIdpId));
        return ToActionResult(result);
    }
    [HttpGet] //Protected for admin and high users
    public async Task<IActionResult> GetRequestsByIqOwnership()
    {
        var userIdpId = _userManager.GetUserId(User)!;
        var result = await _queryHandler.HandleAsync(new GetBindIqRequestsQuery(userIdpId));
        return ToActionResult(result); 
    }
    
    [HttpPost] 
    public async Task<IActionResult> AcceptRequest(AcceptRequestDto dto)
    {
        var userIdpId = _userManager.GetUserId(User)!;
        var result = await _acceptBindRequestHandler.HandleAsync(new AcceptBindRequestCommand(dto.BuildingName, dto.UserToBind, userIdpId));
        return ToActionResult(result);
    }
}