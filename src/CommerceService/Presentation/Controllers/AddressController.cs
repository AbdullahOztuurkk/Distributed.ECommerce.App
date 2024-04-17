namespace CommerceService.API.Controllers;

[ApiController]
[Route("api/addresses")]
[Authorize]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService)
    {
        this._addressService = addressService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BaseResponse<AddressResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]

    public async Task<IActionResult> Get(int id)
    {
        var response = await _addressService.Get(id);
        return Ok(response);
    }

    [HttpGet("GetMyAddresses")]
    [Authorize]
    [ProducesResponseType(typeof(BaseResponse<AddressResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAddressesByUserId()
    {
        var response = await _addressService.GetMyAddresses();
        return Ok(response);
    }

    [HttpPost("Create")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAddressRequestDto dto)
    {
        var response = await _addressService.Create(dto);
        return Ok(response);
    }

    [HttpPut("Update")]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateAddressDto dto)
    {
        var response = await _addressService.Update(dto);
        return Ok(response);
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(BaseResponse<AddressResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _addressService.Delete(id);
        return Ok(response);
    }
}