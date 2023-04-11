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
    public class CouponController : ControllerBase
    {
        private readonly IMediator mediator;
        public CouponController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Coupon>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllCouponsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new GetCouponByIdQuery { Id = id });
            return Ok(result);
        }

        // v1/api/controller/action/2012-12-31
        [HttpGet("GetListByDate/{date}")]
        [ProducesResponseType(typeof(List<Coupon>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCouponsByDate(string date)
        {
            var actualDate = DateTime.Parse(date);
            var result = await mediator.Send(new GetCouponByDateQuery { ExpirationDate = actualDate });
            return Ok(result);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCouponCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateCouponCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromBody] DeleteCouponCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}