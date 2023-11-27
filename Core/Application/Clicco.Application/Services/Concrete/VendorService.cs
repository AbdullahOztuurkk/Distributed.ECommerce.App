using Clicco.Application.Services.Abstract;
using Clicco.Domain.Model.Dtos.Vendor;

namespace Clicco.Application.Services.Concrete
{
    public class VendorService : IVendorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VendorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto> Create(CreateVendorDto dto)
        {
            ResponseDto response = new();

            var isExist = await _unitOfWork.GetRepository<Vendor>().GetAsync(x => x.SlugUrl == dto.Name.AsSlug());
            if (isExist != null)
                return response.Fail(Errors.VendorAlreadyExist);

            Vendor vendor = new()
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                SlugUrl = dto.Name.AsSlug(),
                Address = dto.Address,
                Region = dto.Region,
                CreatedDate = DateTime.UtcNow.AddHours(3),
            };

            await _unitOfWork.GetRepository<Vendor>().AddAsync(vendor);
            await _unitOfWork.SaveChangesAsync();

            return response;
        }

        public async Task<ResponseDto> Delete(int id)
        {
            ResponseDto response = new();
            var vendor = await _unitOfWork.GetRepository<Vendor>().GetByIdAsync(id);
            if(vendor == null)
                response.Fail(Errors.VendorNotFound);

            _unitOfWork.GetRepository<Vendor>().Delete(vendor);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }

        public async Task<ResponseDto> Get(int id)
        {
            ResponseDto response = new();

            var vendor = await _unitOfWork.GetRepository<Vendor>().GetAsync(x => x.Id == id);

            if (vendor == null)
                return response.Fail(Errors.ProductNotFound);

            response.Data = new VendorResponseDto().Map(vendor);

            return response;
        }

        public async Task<ResponseDto> GetAll(PaginationFilter filter)
        {
            ResponseDto response = new();
            filter.PageNumber = filter.PageNumber > 1 ? filter.PageNumber : 1;
            filter.PageSize = filter.PageNumber > 10 ? filter.PageNumber : 10;

            var vendors = await _unitOfWork.GetRepository<Vendor>().PaginateAsync(filter,null);
            var data = vendors.Select(x => new VendorResponseDto().Map(x));

            response.Data = data;
            return response;
        }

        public async Task<ResponseDto> GetByUrl(string uri)
        {
            ResponseDto response = new();

            var vendor = await _unitOfWork.GetRepository<Vendor>().GetAsync(x => x.SlugUrl == uri);

            if (vendor == null)
                return response.Fail(Errors.ProductNotFound);

            response.Data = new VendorResponseDto().Map(vendor);

            return response;
        }

        public async Task<ResponseDto> Update(UpdateVendorDto dto)
        {
            ResponseDto response = new();
            var vendor = await _unitOfWork.GetRepository<Vendor>().GetByIdAsync(dto.Id);
            if (vendor == null)
                response.Fail(Errors.VendorNotFound);

            vendor.Address = dto.Address;
            vendor.Email = dto.Email;
            vendor.Region = vendor.Region;
            vendor.PhoneNumber = dto.PhoneNumber;
            vendor.Name = dto.Name;
            vendor.UpdatedDate = DateTime.UtcNow.AddHours(3);

            _unitOfWork.GetRepository<Vendor>().Update(vendor);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }
    }
}
