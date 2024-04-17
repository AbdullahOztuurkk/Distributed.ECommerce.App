namespace CommerceService.API.Controllers;

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
    [ProducesResponseType(typeof(BaseResponse<ReviewResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]

    public async Task<IActionResult> Get(int id)
    {
        var result = await _reviewService.Get(id);
        return Ok(result);
    }

    [HttpGet("GetByProductId/{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BaseResponse<List<ReviewResponseDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetByProductId(int id)
    {
        var result = await _reviewService.GetAllByProductId(id);
        return Ok(result);
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateReviewRequestDto dto)
    {
        var result = await _reviewService.Create(dto);
        return Ok(result);
    }

    [HttpPut("Update")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateReviewDto dto)
    {
        var result = await _reviewService.Update(dto);
        return Ok(result);
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _reviewService.Delete(id);
        return Ok(result);
    }
}