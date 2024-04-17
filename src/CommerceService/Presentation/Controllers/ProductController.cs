namespace CommerceService.API.Controllers;

[ApiController]
[Route("api/products")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BaseResponse<List<ProductResponseDto>>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BaseResponse<ProductResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _productService.Get(id);
        return Ok(result);
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequestDto dto)
    {
        var result = await _productService.Create(dto);
        return Ok(result);
    }

    [HttpPut("Update")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto dto)
    {
        var result = await _productService.Update(dto);
        return Ok(result);
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.Delete(id);
        return Ok(result);
    }
}