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
    public class AddressController : ControllerBase
    {
        private readonly IMediator mediator;
        public AddressController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Address>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllAddressesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Address), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var address = await mediator.Send(new GetAddressByIdQuery { Id = id });
            return Ok(address);
        }

        [HttpGet("GetByUserId")]
        [ProducesResponseType(typeof(Address), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAddressByUserId(int userId)
        {
            var address = await mediator.Send(new GetAddressesByUserIdQuery { UserId = userId });
            return Ok(address);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody]CreateAddressCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(Address), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateAddressCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromBody] DeleteAddressCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}