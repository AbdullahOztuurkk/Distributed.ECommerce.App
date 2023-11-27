using Clicco.Application.Services.Abstract;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model.Dtos.Transaction;
using Clicco.Domain.Shared.Models.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Clicco.WebAPI.Controllers
{
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
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(CreateTransactionDto paymentRequest)
        {
            var response = await _transactionService.Create(paymentRequest);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDto<TransactionResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var result = await _transactionService.Get(id);
            return Ok(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateTransactionDto dto)
        {
            var result = await _transactionService.Update(dto);
            return Ok(result);
        }

        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _transactionService.Delete(id);
            return Ok(result);
        }

        [HttpGet("{id}/details/SendInvoiceEmail")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetInvoiceEmailByTransactionId([FromRoute] int id)
        {
            var result = await _transactionService.GetInvoiceEmailByTransactionId(id);
            return Ok(result);
        }
    }
}