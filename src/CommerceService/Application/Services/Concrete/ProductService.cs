using CommerceService.Application.Services.Abstract;
using MassTransit;
using Shared.Domain.Constant;
using Shared.Events.Stock;

namespace CommerceService.Application.Services.Concrete;

public class ProductService : BaseService, IProductService
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public ProductService(ISendEndpointProvider sendEndpointProvider)
    {
        this._sendEndpointProvider = sendEndpointProvider;
    }

    public async Task<BaseResponse> Create(CreateProductRequestDto dto)
    {
        BaseResponse response = new();

        var category = await Db.GetDefaultRepo<Category>().GetAsync(x => x.Id == dto.CategoryId);
        if (category == null)
            return response.Fail(Error.E_0000);

        var vendor = await Db.GetDefaultRepo<Vendor>().GetAsync(x => x.Id == dto.VendorId);
        if (vendor == null)
            return response.Fail(Error.E_0000);

        Product product = new()
        {
            CategoryId = dto.CategoryId,
            VendorId = dto.VendorId,
            Description = dto.Description,
            UnitPrice = dto.UnitPrice,
            Code = dto.Code,
            SlugUrl = dto.Name.AsSlug(),
            Name = dto.Name,
        };

        await Db.GetDefaultRepo<Product>().InsertAsync(product);
        await Db.GetDefaultRepo<Product>().SaveChanges();
        Db.Commit();

        var stockEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.CreateProductStockQueue}"));
        await stockEndpoint.Send(new CreateProductStockEvent(ProductId: product.Id, Count: dto.Quantity));

        return response;
    }

    public async Task<BaseResponse> Delete(int id)
    {
        BaseResponse response = new();

        var product = await Db.GetDefaultRepo<Product>().GetByIdAsync(id);
        if (product == null)
            return response.Fail(Error.E_0000);

        product.DeleteDate = DateTime.UtcNow.AddHours(3);
        product.Status = StatusType.PASSIVE;

        await Db.GetDefaultRepo<Product>().SaveChanges();

        var stockEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.DeleteProductStockQueue}"));
        await stockEndpoint.Send(new DeleteProductStockEvent(ProductId: product.Id));

        return response;
    }

    public async Task<BaseResponse> Get(int id)
    {
        BaseResponse response = new();

        var product = await Db.GetDefaultRepo<Product>().GetAsync(x => x.Id == id);

        if (product == null)
            return response.Fail(Error.E_0000);

        var productVendor = await Db.GetDefaultRepo<Product>().GetAsync(x => x.Id == product.VendorId);

        if (productVendor == null)
            return response.Fail(Error.E_0000);

        var productCategory = await Db.GetDefaultRepo<Product>().GetAsync(x => x.Id == product.CategoryId);

        if (productCategory == null)
            return response.Fail(Error.E_0000);

        var data = new ProductResponseDto().Map(product);
        data.CategoryName = productCategory.Name;
        data.VendorName = productVendor.Name;

        response.Data = data;

        return response;
    }

    public async Task<BaseResponse> GetAll()
    {
        BaseResponse response = new();
        var products = await Db.GetDefaultRepo<Product>().GetAllAsync();
        if (products == null)
        {
            return response.Fail(Error.E_0000);
        }
        var data = products.Select(x => new ProductResponseDto().Map(x));
        response.Data = data;
        return response;
    }

    public async Task<BaseResponse> GetByUrl(string uri)
    {
        BaseResponse response = new();

        var product = await Db.GetDefaultRepo<Product>().GetAsync(x => x.SlugUrl == uri);

        if (product == null)
            return response.Fail(Error.E_0000);

        response.Data = new ProductResponseDto().Map(product);

        return response;
    }

    public async Task<BaseResponse> GetByUrl(GetByVendorUrlRequestDto dto)
    {
        BaseResponse response = new();

        var vendor = await Db.GetDefaultRepo<Vendor>().GetAsync(x => x.SlugUrl == dto.VendorUrl);
        if (vendor == null)
            return response.Fail(Error.E_0000);

        var product = await Db.GetDefaultRepo<Product>().GetAsync(x => x.VendorId == vendor.Id && x.SlugUrl == dto.ProductUrl);
        if (product == null)
            return response.Fail(Error.E_0000);

        response.Data = new ProductResponseDto().Map(product);
        return response;
    }

    public async Task<BaseResponse> Update(UpdateProductDto dto)
    {
        BaseResponse response = new();

        var product = await Db.GetDefaultRepo<Product>().GetAsync(x => x.Id == dto.Id);
        if (product == null)
            return response.Fail(Error.E_0000);

        var category = await Db.GetDefaultRepo<Category>().GetAsync(x => x.Id == dto.CategoryId);
        if (category == null)
            return response.Fail(Error.E_0000);

        product.Name = dto.Name;
        product.SlugUrl = dto.Name.AsSlug();
        product.Description = dto.Description;
        product.UnitPrice = dto.UnitPrice;
        product.Code = dto.Code;

        await Db.GetDefaultRepo<Product>().SaveChanges();
        response.Data = new ProductResponseDto().Map(product);

        return response;
    }
}
