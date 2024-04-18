using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Read.Contracts.Queries;
using Read.Implementation.Dto;
using Read.Implementation.Queries.Access;

namespace ClayLocks.Controllers;

[Route("[controller]/[action]")]
[Authorize]
[ApiController]
public class Locks : ApiController
{
    private readonly IQueryHandlerT<GetAllowedLocksQuery, UserIdDto> _allowedLocks;
    private readonly IQueryHandlerT<OpenLockQuery,OpenLockDto> _unlock;
    private readonly UserManager<IdentityUser> _userManager;
    public Locks(
        IQueryHandlerT<GetAllowedLocksQuery, UserIdDto> allowedLocks, 
        IQueryHandlerT<OpenLockQuery,OpenLockDto> unlock, 
        UserManager<IdentityUser> userManager)
    {
        _allowedLocks = allowedLocks;
        _unlock = unlock;
        _userManager = userManager;
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
}