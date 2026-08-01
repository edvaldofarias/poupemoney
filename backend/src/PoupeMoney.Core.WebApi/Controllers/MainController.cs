using PoupeMoney.Core.Application.Commons;

namespace PoupeMoney.Core.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
[ApiConventionType(typeof(ApiConventions))]
public class MainController : ControllerBase
{
    internal IActionResult Result(Response response)
    {
        if (response.Success)
            return NoContent();
        return UnprocessableEntity(response.Errors);
    }

    internal IActionResult Result<T>(Response<T> response)
    {
        if (response.Success)
            return Ok(response.Data);
        else
            return UnprocessableEntity(response.Errors);
    }

    internal IActionResult Result<T>(Response<T> response, string actionName)
    {
        if (response.Success)
            return CreatedAtAction(actionName, response.Data);
        return UnprocessableEntity(response.Errors);
    }
}