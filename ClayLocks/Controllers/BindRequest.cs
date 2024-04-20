using ClayLocks.IDP;
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
    private readonly IQueryHandler<GetBindIqRequestsQuery> _queryHandler;
    private readonly Read.Contracts.Commands.ICommandHandler<CreateBindRequestCommand> _createBindRequestHandler;
    private readonly ICommandHandler<AcceptBindRequestCommand> _acceptBindRequestHandler;

    public BindRequest(
        IQueryHandler<GetBindIqRequestsQuery> queryHandler,
        Read.Contracts.Commands.ICommandHandler<CreateBindRequestCommand> createBindRequestHandler,
        ICommandHandler<AcceptBindRequestCommand> acceptBindRequestHandler,
        UserManager<IdentityUser> userManager) :base (userManager)
        
    {
        _queryHandler = queryHandler;
        _createBindRequestHandler = createBindRequestHandler;
        _acceptBindRequestHandler = acceptBindRequestHandler;
    }
    
    [Authorize]
    [HttpPost] 
    public async Task<IActionResult> Add(BindRequestDto dto)
    {
        var result = await _createBindRequestHandler
            .HandleAsync(new CreateBindRequestCommand(dto.BuildingName,GetUserId(User)));
        return ToActionResult(result);
    }
    
    [Authorize(Roles=Roles.Admin)]
    [HttpGet] 
    public async Task<IActionResult> GetAll()
    {
        var result = await _queryHandler.HandleAsync();
        return ToActionResult(result); 
    }
    
    [Authorize(Roles=Roles.Admin)]
    [HttpPost] 
    public async Task<IActionResult> Accept(AcceptBindRequestDto dto)
    {
        var result = await _acceptBindRequestHandler.HandleAsync(new AcceptBindRequestCommand(dto.BuildingName, dto.UserToBind));
        return ToActionResult(result);
    }
}