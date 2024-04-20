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
    public async Task<IActionResult> CreateUpgradeRequest(HigherAccessDto dto)
    {
        var userIdpId = _userManager.GetUserId(User)!;
        var result = await _accessHandler.HandleAsync(new CreateHigherAccessRequestCommand(userIdpId, dto.IqId));
        return ToActionResult(result);
    }
    
    [HttpGet] 
    public async Task<IActionResult> GetRequests()
    {
        var query = new GetAccessRequestDto()
        {
            UserId = _userManager.GetUserId(User)!,
            IsAdmin =  IsAdmin(User)
        };
        return ToActionResult(await _accessRequestQueryHandler.HandleAsync(query));
    }
    [HttpPost] 
    public async Task<IActionResult> AcceptRequest(AcceptHigherAccessDto dto)
    {
        var result = await _acceptHigher.HandleAsync(new AcceptHigherAccessCommand(dto.UserId, _userManager.GetUserId(User)!, IsAdmin(User)));
        return ToActionResult(result);
    }
    private static bool IsAdmin(ClaimsPrincipal user)
    {
        var userIdentity = (ClaimsIdentity)user.Identity!;
        var claims = userIdentity.Claims;
        var roleClaimType = userIdentity.RoleClaimType;
        var claimRoles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
        var roles =claimRoles.Select(c => c.Value);
        return roles.Contains(Roles.Admin);
    } 
}