using Clicco.Application.Features.Commands;
using Clicco.Application.Features.Queries;
using Clicco.Application.Features.Queries.Transactions;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core.ResponseModel;
using Clicco.WebAPI.Models;
using MediatR;
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
        private readonly IMediator mediator;
        public TransactionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TransactionViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new GetTransactionByIdQuery { Id = id });
            return Ok(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(TransactionViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateTransactionCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(TransactionDetailViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await mediator.Send(new GetTransactionDetailByTransactionIdQuery { Id  = id });
            return Ok(result);
        }

        [HttpGet("{id}/details/SendInvoiceEmail")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoiceEmailByTransactionId([FromRoute] int id)
        {
            var result = await mediator.Send(new GetInvoiceEmailByTransactionIdQuery { TransactionId = id });
            return Ok(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromBody] DeleteTransactionCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}