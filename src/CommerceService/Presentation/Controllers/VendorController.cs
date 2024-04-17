namespace CommerceService.API.Controllers;

[Route("api/vendors")]
[ApiController]
public class VendorController : ControllerBase
{
    private readonly IVendorService _vendorService;

    public VendorController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(BaseResponse<List<VendorResponseDto>>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll()
    {
        var addresses = await _vendorService.GetAll();
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BaseResponse<VendorResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]

    public async Task<IActionResult> Get(int id)
    {
        var result = await _vendorService.Get(id);
        return Ok(result);
    }

    [HttpGet("/url/{url}")]
    [ProducesResponseType(typeof(BaseResponse<VendorResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]

    public async Task<IActionResult> GetByUrl(string url)
    {
        var result = await _vendorService.GetByUrl(url);
        return Ok(result);
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateVendorDto dto)
    {
        var result = await _vendorService.Create(dto);
        return Ok(result);
    }

    [HttpPut("Update")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateVendorDto dto)
    {
        var result = await _vendorService.Update(dto);
        return Ok(result);
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _vendorService.Delete(id);
        return Ok(result);
    }
}
