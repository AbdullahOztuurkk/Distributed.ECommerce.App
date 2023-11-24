using Clicco.Domain.Model.Dtos.Address;

namespace Clicco.Application.Services.Abstract
{
    public interface IAddressService
    {
        Task<ResponseDto> Create(CreateAddressDto dto);
        Task<ResponseDto> Update(UpdateAddressDto dto);
        Task<ResponseDto> Delete(int id);
        Task<ResponseDto> Get(int id);
        Task<ResponseDto> GetMyAddresses();
    }
}
