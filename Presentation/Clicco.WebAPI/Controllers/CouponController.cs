﻿using Clicco.Application.Features.Commands;
using Clicco.Application.Features.Queries;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using Clicco.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Clicco.WebAPI.Controllers
{
    [ApiController]
    [Route("api/coupons")]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly IMediator mediator;
        public CouponController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<CouponViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllCouponsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CouponViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new GetCouponByIdQuery { Id = id });
            return Ok(result);
        }

        // v1/api/controller/action/2012-12-31
        [HttpGet("GetListByDate/{date}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<CouponViewModel>), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(CouponViewModel), (int)HttpStatusCode.OK)]
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