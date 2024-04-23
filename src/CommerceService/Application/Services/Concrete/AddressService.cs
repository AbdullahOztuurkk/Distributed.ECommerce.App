using CommerceService.Application.Services.Abstract;

namespace CommerceService.Application.Services.Concrete;

public class AddressService : BaseService, IAddressService
{
    public async Task<BaseResponse> Create(CreateAddressRequestDto dto)
    {
        Address address = new()
        {
            State = dto.State,
            City = dto.City,
            ZipCode = dto.ZipCode,
            Country = dto.Country,
            Street = dto.Street,
            UserId = CurrentUser.Id,
        };

        await Db.GetDefaultRepo<Address>().InsertAsync(address);
        await Db.GetDefaultRepo<Address>().SaveChanges();
        Db.Commit();

        return new BaseResponse();
    }

    public async Task<BaseResponse> Delete(int id)
    {
        BaseResponse response = new();

        var address = await Db.GetDefaultRepo<Address>().GetAsync(x => x.Id == id);
        if (address == null)
            return response.Fail(Error.E_0000);

        address.DeleteDate = DateTime.UtcNow.AddHours(3);
        address.Status = StatusType.ACTIVE;

        await Db.GetDefaultRepo<Address>().SaveChanges();
        return response;
    }

    public async Task<BaseResponse> Get(int id)
    {
        BaseResponse response = new();

        var address = await Db.GetDefaultRepo<Address>().GetAsync(x => x.Id == id);

        if (address == null)
            return response.Fail(Error.E_0000);

        response.Data = new AddressResponseDto().Map(address);

        return response;
    }

    public async Task<BaseResponse> GetMyAddresses()
    {
        BaseResponse response = new();
        var reviews = await Db.GetDefaultRepo<Address>().GetManyAsync(x => x.UserId == CurrentUser.Id);
        var data = reviews.Select(x => new AddressResponseDto().Map(x));
        response.Data = data;
        return response;
    }

    public async Task<BaseResponse> Update(UpdateAddressDto dto)
    {
        BaseResponse response = new();
        var address = await Db.GetDefaultRepo<Address>().GetAsync(x => x.Id == dto.Id);
        if (address == null)
            return response.Fail(Error.E_0000);

        address.Street = dto.Street;
        address.City = dto.City;
        address.State = dto.State;
        address.Country = dto.Country;
        address.ZipCode = dto.ZipCode;
        address.UserId = CurrentUser.Id;

        await Db.GetDefaultRepo<Address>().SaveChanges();

        var cacheKey = string.Format(CacheKeys.Address, dto.Id);

        await CacheService.SetAsync(cacheKey, address);

        response.Data = new AddressResponseDto().Map(address);

        return response;
    }
}
