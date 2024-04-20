using System.Security.Claims;
using ClayLocks.IDP;
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
using Write.Implementation.Dto;

namespace ClayLocks.Controllers;

[Route("[controller]/[action]")]
[Authorize]
[ApiController]
public class Access : ApiController
{
    private readonly ICommandHandler<CreateHigherAccessRequestCommand> _accessHandler;
    private readonly IQueryHandlerT<GetAccessRequestsQuery, GetAccessRequest> _accessRequestQueryHandler;
    private readonly Write.Contacts.Commands.ICommandHandler<AcceptHigherAccessCommand> _acceptHigher;
    public Access(
        ICommandHandler<CreateHigherAccessRequestCommand> accessHandler, 
        IQueryHandlerT<GetAccessRequestsQuery,GetAccessRequest> accessRequestQueryHandler, 
        Write.Contacts.Commands.ICommandHandler<AcceptHigherAccessCommand> acceptHigher,
        UserManager<IdentityUser> userManager 
    ):base(userManager)
    {
        _accessHandler = accessHandler;
        _accessRequestQueryHandler = accessRequestQueryHandler;
        _acceptHigher = acceptHigher;
    }

    [HttpPost] 
    public async Task<IActionResult> CreateUpgradeRequest(HigherAccessDto dto)
    {
        var result = await _accessHandler.HandleAsync(new CreateHigherAccessRequestCommand(GetUserId(User), dto.IqId));
        return ToActionResult(result);
    }
    
    [HttpGet] 
    public async Task<IActionResult> GetRequests()
    {
        var query = new GetAccessRequest(GetUserId(User), IsAdmin(User));
        
        return ToActionResult(await _accessRequestQueryHandler.HandleAsync(query));
    }
    [HttpPost] 
    public async Task<IActionResult> AcceptRequest(AcceptHigherAccessDto dto)
    {
        var command = new AcceptHigherAccessCommand(dto.UserId, GetUserId(User), IsAdmin(User));
        var result = await _acceptHigher.HandleAsync(command);
        return ToActionResult(result);
    }
}