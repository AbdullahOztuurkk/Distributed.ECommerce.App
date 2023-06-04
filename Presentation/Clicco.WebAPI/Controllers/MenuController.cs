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
    [Route("api/menus")]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMediator mediator;
        public MenuController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<MenuViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter)
        {
            var result = await mediator.Send(new GetAllMenusQuery(paginationFilter));
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(MenuViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new GetMenuByIdQuery { Id = id });
            return Ok(result);
        }

        // v1/api/controller/action/clicco-e-commerce-menu-url
        [HttpGet("GetByUrl/{url}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<MenuViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMenuByUrl(string url)
        {
            var result = await mediator.Send(new GetMenuByUrlQuery { Url = url });
            return Ok(result);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse<MenuViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMenuCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(BaseResponse<MenuViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateMenuCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(BaseResponse<MenuViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromBody] DeleteMenuCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}