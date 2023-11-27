using Clicco.Application.Services.Abstract;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model.Dtos.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Clicco.WebAPI.Controllers
{
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
        [ProducesResponseType(typeof(ResponseDto<AddressResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Get(int id)
        {
            var response = await _addressService.Get(id);
            return Ok(response);
        }

        [HttpGet("GetMyAddresses")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseDto<AddressResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAddressesByUserId()
        {
            var response = await _addressService.GetMyAddresses();
            return Ok(response);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateAddressDto dto)
        {
            var response = await _addressService.Create(dto);
            return Ok(response);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateAddressDto dto)
        {
            var response = await _addressService.Update(dto);
            return Ok(response);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(ResponseDto<AddressResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _addressService.Delete(id);
            return Ok(response);
        }
    }
}