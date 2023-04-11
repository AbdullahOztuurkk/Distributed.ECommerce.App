using Clicco.Application.Features.Commands;
using Clicco.Application.Features.Queries;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using Clicco.WebAPI.Models;
using Clicco.WebAPI.NewFolder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Clicco.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMediator mediator;
        public MenuController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Menu>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllMenusQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new GetMenuByIdQuery { Id = id });
            return Ok(result);
        }

        // v1/api/controller/action/2012-12-31
        [HttpGet("GetByUrl/{url}")]
        [ProducesResponseType(typeof(List<Menu>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMenuByUrl(string url)
        {
            var result = await mediator.Send(new GetMenuByUrlQuery { Url = url });
            return Ok(result);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMenuCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(Menu), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateMenuCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromBody] DeleteMenuCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}