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
         return BadRequest(result.Error); // in prod i should only return the code of the error.
      }
      return Ok();
   }
   
   protected IActionResult ToActionResult<T>(Result<T> result)
   {
      if (!result.IsSuccess)
      {
         return BadRequest(result.Error);// in prod i should only return the code of the error.
      }
      if(result.Value is null)
      {
         return NoContent();
      }
      return Ok(result.Value);
   }
}
