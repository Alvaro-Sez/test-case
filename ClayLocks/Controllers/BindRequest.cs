using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Write.Contacts.Commands;
using Write.Contacts.Dto;
using Write.Contacts.Entities;
using Write.Contacts.Queries;
using Write.Implementation.Commands.BindRequests;
using Write.Implementation.Queries.BindRequest;

namespace ClayLocks.Controllers;

[Route("bind")]
[ApiController]
public class BindRequest : ApiController
{
    private readonly IQueryHandler<GetBindIqRequestsQuery,IEnumerable<BindIqRequest>> _queryHandler;
    private readonly ICommandHandler<CreateBindRequestCommand> _commandHandler;
   private readonly UserManager<IdentityUser> _userManager;
   private readonly ICommandHandler<CreateBindRequestCommand> _createBindRequestHandler;

   public BindRequest(
        IQueryHandler<GetBindIqRequestsQuery,IEnumerable<BindIqRequest>> queryHandler,
        ICommandHandler<CreateBindRequestCommand> commandHandler, 
        UserManager<IdentityUser> userManager,
        ICommandHandler<CreateBindRequestCommand> createBindRequestHandler)
    {
        _queryHandler = queryHandler;
        _commandHandler = commandHandler;
        _userManager = userManager;
        _createBindRequestHandler = createBindRequestHandler;
    }
    [HttpPost] 
    public async Task<IActionResult> CreateBindRequest(BindRequestDto dto)
    {
        var result = await _createBindRequestHandler.HandleAsync(new CreateBindRequestCommand(dto.BuildingName, dto.UserToBind));
        return await RespondAsync(result);
    }
    [HttpGet] 
    public async Task<IActionResult> GetRequests()
    {
        var user = _userManager.GetUserId(User);
        var result = await _queryHandler.HandleAsync(new GetBindIqRequestsQuery(user));
        return await RespondAsync(result); 
    }
    
    [HttpPost] 
    public Task<IActionResult> AcceptRequest()
    {
        throw new NotImplementedException();
    }
}