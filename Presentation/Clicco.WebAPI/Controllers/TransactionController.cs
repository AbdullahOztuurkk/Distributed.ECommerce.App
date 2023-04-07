using Clicco.Application.Features.Commands;
using Clicco.Application.Features.Queries;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using Clicco.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Clicco.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator mediator;
        public TransactionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Transaction>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await mediator.Send(new GetAllMenusQuery());
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transaction), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new GetTransactionByIdQuery { Id = id });
            return Ok(result);
        }

        // v1/api/controller/action/2012-12-31
        //[HttpGet("GetListByDate/{date}")]
        //[ProducesResponseType(typeof(List<Transaction>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> GetTransactionsByDate(string date)
        //{
        //    var result = await mediator.Send(new GetTransactionsByDateQuery { Date = date });
        //    return Ok(result);
        //}

        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(Transaction), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateTransactionCommand command)
        {
            var result = await mediator.Send(command);
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