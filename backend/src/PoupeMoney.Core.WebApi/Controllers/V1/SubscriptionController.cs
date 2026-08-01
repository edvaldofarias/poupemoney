using PoupeMoney.Core.Application.Commands.Subscription;

namespace PoupeMoney.Core.WebApi.Controllers.V1;

[ApiVersion("1.0")]
public sealed class SubscriptionController(
    ISubscriptionService subscriptionService,
    ILogger<SubscriptionController> logger) : MainController
{
    /// <summary>
    ///     Criando uma sub inscrição para o usuário
    /// </summary>
    /// <param name="command">Dados necessário para o cadastro de uma sub inscrição</param>
    /// <param name="cancellationToken">Token de cancelamento do processo</param>
    /// <returns></returns>
    /// <response code="204">Sub inscrição criada com sucesso</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="422">Dados inválidos</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SubscriptionCreateCommand command, CancellationToken cancellationToken)
    {
        var response = await subscriptionService.CreateAsync(command, cancellationToken);

        logger.LogInformation(
            "Process create subscription with {@SubscriptionCreateCommand} with response {@Response}",
            command, response);

        return Result(response);
    }

    /// <summary>
    ///     Recuperando a sub inscrição do usuário conectado
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento do processo</param>
    /// <returns>Sub inscrição do usuário</returns>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="501">Método não implementado</response>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get(CancellationToken cancellationToken)
    {
        return new StatusCodeResult(StatusCodes.Status501NotImplemented);
    }
}