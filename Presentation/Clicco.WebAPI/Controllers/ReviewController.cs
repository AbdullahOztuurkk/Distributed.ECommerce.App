using Clicco.Application.Services.Abstract;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model.Dtos.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Clicco.Domain.Shared.Global;

namespace Clicco.WebAPI.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseDto<ReviewResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var result = await _reviewService.Get(id);
            return Ok(result);
        }

        [HttpGet("GetByProductId/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseDto<List<ReviewResponseDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByProductId([FromQuery] PaginationFilter filter, int id)
        {
            var result = await _reviewService.GetAllByProductId(id, filter);
            return Ok(result);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
        {
            var result = await _reviewService.Create(dto);
            return Ok(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateReviewDto dto)
        {
            var result = await _reviewService.Update(dto);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reviewService.Delete(id);
            return Ok(result);
        }
    }
}