using Clicco.Application.Services.Abstract;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model.Dtos.Coupon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Clicco.Domain.Shared.Global;

namespace Clicco.WebAPI.Controllers
{
    [ApiController]
    [Route("api/coupons")]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseDto<List<CouponResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter)
        {
            var response = await _couponService.GetAll(paginationFilter);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseDto<CouponResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var response = await _couponService.Get(id);
            return Ok(response);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCouponDto dto)
        {
            var response = await _couponService.Create(dto);
            return Ok(response);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateCouponDto dto)
        {
            var response = await _couponService.Update(dto);
            return Ok(response);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _couponService.Delete(id);
            return Ok(response);
        }
    }
}