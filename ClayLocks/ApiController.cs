using Microsoft.AspNetCore.Mvc;
using Shared;

namespace ClayLocks;

[ApiController]
public abstract class ApiController : ControllerBase
{

   protected async Task<IActionResult> RespondAsync(Result result)
   {
      if (!result.IsSuccess)
      {
         return BadRequest(result.Error.Code);
      }
      return Ok();
   }
   
   protected async Task<IActionResult> RespondAsync<T>(Result<T> result)
   {
      if (!result.IsSuccess)
      {
         return BadRequest(result.Error.Code);
      }
      return Ok(result.Value);
   }
}