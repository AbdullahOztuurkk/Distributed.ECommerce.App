using CommerceService.Application.Services.Abstract;

namespace CommerceService.Application.Services.Concrete;

public class VendorService : BaseService, IVendorService
{
    private readonly IUnitOfWork _unitOfWork;
    public VendorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<BaseResponse> Create(CreateVendorDto dto)
    {
        BaseResponse response = new();

        var isExist = await Db.GetDefaultRepo<Vendor>().GetAsync(x => x.SlugUrl == dto.Name.AsSlug());
        if (isExist != null)
            return response.Fail(Error.E_0002);

        Vendor vendor = new()
        {
            Name = dto.Name,
            PhoneNumber = dto.PhoneNumber,
            SlugUrl = dto.Name.AsSlug(),
            Address = dto.Address,
            Region = dto.Region,
        };

        await Db.GetDefaultRepo<Vendor>().InsertAsync(vendor);
        await Db.GetDefaultRepo<Vendor>().SaveChanges();
        Db.Commit();

        return response;
    }

    public async Task<BaseResponse> Delete(int id)
    {
        BaseResponse response = new();
        var vendor = await Db.GetDefaultRepo<Vendor>().GetByIdAsync(id);
        if (vendor == null)
            response.Fail(Error.E_0000);

        vendor.DeleteDate = DateTime.UtcNow.AddHours(3);
        vendor.Status = StatusType.PASSIVE;

        await Db.GetDefaultRepo<Vendor>().SaveChanges();
        return response;
    }

    public async Task<BaseResponse> Get(int id)
    {
        BaseResponse response = new();

        var vendor = await Db.GetDefaultRepo<Vendor>().GetAsync(x => x.Id == id);

        if (vendor == null)
            return response.Fail(Error.E_0000);

        response.Data = new VendorResponseDto().Map(vendor);

        return response;
    }

    public async Task<BaseResponse> GetAll()
    {
        BaseResponse response = new();
        var vendorrs = await Db.GetDefaultRepo<Vendor>().GetAllAsync();
        if (vendorrs == null)
        {
            return response.Fail(Error.E_0000);
        }
        var data = vendorrs.Select(x => new VendorResponseDto().Map(x));
        response.Data = data;
        return response;
    }

    public async Task<BaseResponse> GetByUrl(string uri)
    {
        BaseResponse response = new();

        var vendor = await Db.GetDefaultRepo<Vendor>().GetAsync(x => x.SlugUrl == uri);

        if (vendor == null)
            return response.Fail(Error.E_0000);

        response.Data = new VendorResponseDto().Map(vendor);

        return response;
    }

    public async Task<BaseResponse> Update(UpdateVendorDto dto)
    {
        BaseResponse response = new();
        var vendor = await Db.GetDefaultRepo<Vendor>().GetByIdAsync(dto.Id);
        if (vendor == null)
            response.Fail(Error.E_0000);

        vendor.Address = dto.Address;
        vendor.Email = dto.Email;
        vendor.Region = vendor.Region;
        vendor.PhoneNumber = dto.PhoneNumber;
        vendor.Name = dto.Name;

        Db.GetDefaultRepo<Vendor>().SaveChanges();
        
        return response;
    }
}
