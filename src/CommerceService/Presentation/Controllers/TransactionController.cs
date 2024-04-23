using Shared.Events.Payment;

namespace CommerceService.API.Controllers;

[ApiController]
[Route("api/transactions")]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        this._transactionService = transactionService;
    }

    [HttpPost]
    [Route("Create")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create(CreateTransactionRequestDto paymentRequest)
    {
        var response = await _transactionService.Create(paymentRequest);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BaseResponse<TransactionResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]

    public async Task<IActionResult> Get(int id)
    {
        var result = await _transactionService.Get(id);
        return Ok(result);
    }

    [HttpGet("{id}/details/SendInvoiceEmail")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetInvoiceEmailByTransactionId([FromRoute] int id)
    {
        var result = await _transactionService.GetInvoiceEmailByTransactionId(id);
        return Ok(result);
    }
}