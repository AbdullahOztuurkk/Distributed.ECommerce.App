using Clicco.Application.Services.Abstract;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model.Dtos.Menu;
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
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseDto<List<MenuResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(PaginationFilter filter)
        {
            var response = await _menuService.GetAll(filter);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseDto<MenuResponseDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> Get(int id)
        {
            var response = await _menuService.Get(id);
            return Ok(response);
        }

        [HttpGet("GetByUrl/{url}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseDto<List<MenuResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMenuByUrl(string url)
        {
            var response = await _menuService.GetByUrl(url);
            return Ok(response);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateMenuDto dto)
        {
            var response = await _menuService.Create(dto);
            return Ok(response);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateMenuDto dto)
        {
            var response = await _menuService.Update(dto);
            return Ok(response);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _menuService.Delete(id);
            return Ok(response);
        }
    }
}