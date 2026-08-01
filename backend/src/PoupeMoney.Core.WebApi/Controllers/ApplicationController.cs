namespace PoupeMoney.Core.WebApi.Controllers;

[ApiVersion("1.0")]
[ExcludeFromCodeCoverage]
public sealed class ApplicationController(ILogger<ApplicationController> logger) : MainController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public IActionResult WarmUp()
    {
        logger.LogDebug("Warm Up starting ...");
        return Ok();
    }
}
