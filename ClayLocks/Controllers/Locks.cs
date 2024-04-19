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
using Write.Implementation.Commands.Locks.Handlers;
using Write.Implementation.Dto;

namespace ClayLocks.Controllers;

[Route("[controller]/[action]")]
[Authorize]
[ApiController]
public class Locks : ApiController
{
    private readonly IQueryHandlerT<GetAllowedLocksQuery, UserIdDto> _allowedLocks;
    private readonly IQueryHandlerT<OpenLockQuery,OpenLockDto> _unlock;
    private readonly IQueryHandlerT<GetEventsQuery,GetEventsDto> _events;
    private readonly ICommandHandler<UpgradeSecurityCommand> _securityUpgradeHandler;
    
    private readonly UserManager<IdentityUser> _userManager;
    public Locks(
        IQueryHandlerT<GetAllowedLocksQuery, UserIdDto> allowedLocks, 
        IQueryHandlerT<OpenLockQuery,OpenLockDto> unlock, 
        UserManager<IdentityUser> userManager, IQueryHandlerT<GetEventsQuery, GetEventsDto> events, ICommandHandler<UpgradeSecurityCommand> securityUpgradeHandler)
    {
        _allowedLocks = allowedLocks;
        _unlock = unlock;
        _userManager = userManager;
        _events = events;
        _securityUpgradeHandler = securityUpgradeHandler;
    }
    
    [HttpPost] 
    public async Task<IActionResult> OpenLock(OpenLockDto dto)
    {
        return ToActionResult(await _unlock.HandleAsync(dto));
    }
    
    [HttpGet] 
    public async Task<IActionResult> GetAll()
    {
        var userIdpId = _userManager.GetUserId(User)!;
        return ToActionResult(await _allowedLocks.HandleAsync(new UserIdDto(userIdpId)));
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpPost] 
    public async Task<IActionResult> GetEvents(GetEventsDto dto)
    {
        return ToActionResult(await _events.HandleAsync(dto));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> UpgradeSecurity(UpgradeSecurityDto dto)
    {
        var userIdpId = _userManager.GetUserId(User)!;
        return ToActionResult(await _securityUpgradeHandler.HandleAsync(new UpgradeSecurityCommand(userIdpId, dto.LockId)));
    }
}