using Clicco.Domain.Model.Dtos.Vendor;

namespace Clicco.Application.Services.Abstract
{
    public interface IVendorService
    {
        Task<ResponseDto> Create(CreateVendorDto dto);
        Task<ResponseDto> Update(UpdateVendorDto dto);
        Task<ResponseDto> Delete(int id);
        Task<ResponseDto> Get(int id);
        Task<ResponseDto> GetAll(PaginationFilter filter);
        Task<ResponseDto> GetByUrl(string url);
    }
}
