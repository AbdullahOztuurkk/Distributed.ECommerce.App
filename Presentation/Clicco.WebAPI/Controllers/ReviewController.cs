using Clicco.Application.Features.Commands;
using Clicco.Application.Features.Commands.Reviews;
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
    public class ReviewController : ControllerBase
    {
        private readonly IMediator mediator;
        public ReviewController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Review), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new GetReviewByIdQuery { Id = id });
            return Ok(result);
        }

        // v1/api/controller/action/2012-12-31
        [HttpGet("GetByProductId/{id}")]
        [ProducesResponseType(typeof(List<Review>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMenuByUrl(int id)
        {
            var result = await mediator.Send(new GetReviewsByProductIdQuery { ProductId = id });
            return Ok(result);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateReviewCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(Menu), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateReviewCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DynamicResponseModel), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromBody] DeleteReviewCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}