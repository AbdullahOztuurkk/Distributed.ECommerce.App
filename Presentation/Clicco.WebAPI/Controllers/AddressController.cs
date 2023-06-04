using Clicco.Application.Features.Commands;
using Clicco.Application.Features.Queries;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core.ResponseModel;
using Clicco.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Clicco.Domain.Shared.Global;

namespace Clicco.WebAPI.Controllers
{
    [ApiController]
    [Route("api/addresses")]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IMediator mediator;
        public AddressController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<AddressViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter)
        {
            var result = await mediator.Send(new GetAllAddressesQuery(paginationFilter));
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AddressViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var address = await mediator.Send(new GetAddressByIdQuery { Id = id });
            return Ok(address);
        }

        [HttpGet("GetByUserId")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AddressViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAddressByUserId(int userId)
        {
            var address = await mediator.Send(new GetAddressesByUserIdQuery { UserId = userId });
            return Ok(address);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse<AddressViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody]CreateAddressCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(BaseResponse<AddressViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateAddressCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(BaseResponse<AddressViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromBody] DeleteAddressCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}