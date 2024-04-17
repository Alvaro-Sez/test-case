using Microsoft.AspNetCore.Mvc;
using Shared;

namespace ClayLocks;

[ApiController]
public abstract class ApiController : ControllerBase
{

   protected IActionResult ToActionResult(Result result)
   {
      if (!result.IsSuccess)
      {
         return BadRequest(result.Error.Code);
      }
      return Ok();
   }
   
   protected IActionResult ToActionResult<T>(Result<T> result)
   {
      if (!result.IsSuccess)
      {
         return BadRequest(result.Error.Code);
      }
      return Ok(result.Value);
   }
}
