using PoupeMoney.Core.Application.Queries.Bank;

namespace PoupeMoney.Core.WebApi.Controllers.V1;

public sealed class BankController(
    ILogger<BankController> logger,
    IBankService bankService) : MainController
{
    /// <summary>
    /// Recupera todos os bancos
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento do processo</param>
    /// <response code="200">Retorna os bancos</response>
    /// <response code="401">Não autorizado</response>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BankQuery>>> Get(CancellationToken cancellationToken)
    {
        var banks = await bankService.GetAllAsync(cancellationToken);
        return Ok(banks);
    }

    /// <summary>
    /// Recupera um banco pelo seu Id
    /// </summary>
    /// <param name="id">identificador único do banco</param>
    /// <param name="cancellationToken">Token de cancelamento do processo</param>
    /// <response code="200">Retorna o banco</response>
    /// <response code="401">Não autorizado</response>
    /// <response code="404">Se não existir um banco com este Id</response>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BankQuery>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var bank = await bankService.GetByIdAsync(id, cancellationToken);
        if (bank is not null)
            return Ok(bank);

        logger.LogInformation("bank not found {id}", id);
        return NotFound();
    }
}
