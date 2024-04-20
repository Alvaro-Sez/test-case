using ClayLocks.IDP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Read.Contracts.Queries;
using Read.Implementation.Dto;
using Read.Implementation.Queries.Events;
using Read.Implementation.Queries.Locks;
using Write.Contacts.Commands;
using Write.Implementation.Commands.Locks;
using Write.Implementation.Dto;

namespace ClayLocks.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class Locks : ApiController
{
    private readonly IQueryHandlerT<GetAllowedLocksQuery, UserIdDto> _allowedLocks;
    private readonly IQueryHandlerT<OpenLockQuery,OpenLockDto> _unlock;
    private readonly IQueryHandlerT<GetEventsQuery,GetEventsDto> _events;
    private readonly ICommandHandler<UpgradeSecurityCommand> _securityUpgradeHandler;
    
    public Locks(
        IQueryHandlerT<GetAllowedLocksQuery, UserIdDto> allowedLocks, 
        IQueryHandlerT<OpenLockQuery,OpenLockDto> unlock, 
        IQueryHandlerT<GetEventsQuery, GetEventsDto> events, 
        ICommandHandler<UpgradeSecurityCommand> securityUpgradeHandler,
        UserManager<IdentityUser> userManager): base(userManager)
    {
        _allowedLocks = allowedLocks;
        _unlock = unlock;
        _events = events;
        _securityUpgradeHandler = securityUpgradeHandler;
    }
    
    [Authorize] 
    [HttpPost] 
    public async Task<IActionResult> OpenLock(OpenLockDtoRequest dto)
    {
        var userId = GetUserId(User);
        return ToActionResult(await _unlock.HandleAsync(new OpenLockDto(userId,  dto.LockId!)));
    }
    
    [Authorize] 
    [HttpGet] 
    public async Task<IActionResult> GetAll()
    {
        var userIdpId = GetUserId(User);
        return ToActionResult(await _allowedLocks.HandleAsync(new UserIdDto(userIdpId)));
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpGet("{userId}")] 
    public async Task<IActionResult> GetEvents(string userId)
    {
        return ToActionResult(await _events.HandleAsync(new GetEventsDto(){UserId = userId}));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UpgradeSecurity(UpgradeSecurityDto dto)
    {
        var userIdpId = GetUserId(User);
        var result = await _securityUpgradeHandler.HandleAsync(new UpgradeSecurityCommand(userIdpId, dto.LockId));
        return ToActionResult(result);
    }
}