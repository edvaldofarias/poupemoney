using PoupeMoney.Core.Application.Commands.Account;
using PoupeMoney.Core.Application.Queries.Account;

namespace PoupeMoney.Core.WebApi.Controllers.V1;

public sealed class AccountController(
    ILogger<AccountController> logger,
    IAccountService accountService) : MainController
{
    /// <summary>
    ///    Obtendo todas as contas do usuário
    /// </summary>
    /// <response code="200">Retorna todas as contas do usuário</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountQuery>>> Get(CancellationToken cancellationToken)
    {
        var accounts = await accountService.GetAllAsync(cancellationToken);
        return Ok(accounts);
    }

    /// <summary>
    ///    Obtendo uma conta pelo seu identificador
    /// </summary>
    /// <param name="id">Identificador único da conta</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Retorna a conta do usuário</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <response code="404">Conta não encontrada</response>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AccountQuery>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var account = await accountService.GetByIdAsync(id, cancellationToken);
        return account is null ? NotFound() : Ok(account);
    }

    /// <summary>
    ///     Criando uma conta para o usuário
    /// </summary>
    /// <param name="command"> Comando para cadastro de uma nova conta </param>
    /// <response code="201">Conta criada com sucesso</response>
    /// <response code="422">Dados inválidos</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AccountCreateCommand command)
    {
        logger.LogInformation("Created new account");
        var response = await accountService.CreateAsync(command, CancellationToken.None);

        return Result(response, nameof(GetById));
    }

    /// <summary>
    ///     Atualizando uma conta para o usuário
    /// </summary>
    /// <param name="command"> Comando para atualizar uma conta </param>
    /// <response code="204">Conta atualizada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Usuário não autenticado</response>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] AccountUpdateCommand command)
    {
        logger.LogInformation("Updated account");
        var response = await accountService.UpdateAsync(command, CancellationToken.None);

        return Result(response);
    }
}
