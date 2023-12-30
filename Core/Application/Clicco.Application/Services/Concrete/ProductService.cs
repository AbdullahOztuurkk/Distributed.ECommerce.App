using Clicco.Application.Services.Abstract;
using Clicco.Domain.Core.Extensions;
using Clicco.Domain.Model.Dtos.Menu;
using Clicco.Domain.Model.Dtos.Product;

namespace Clicco.Application.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto> Create(CreateProductDto dto)
        {
            ResponseDto response = new();

            var category = await _unitOfWork.GetRepository<Category>().GetAsync(x => x.Id == dto.CategoryId);
            if (category == null)
                return response.Fail(Errors.CategoryNotFound);

            var vendor = await _unitOfWork.GetRepository<Vendor>().GetAsync(x => x.Id == dto.VendorId);
            if (vendor == null)
                return response.Fail(Errors.VendorNotFound);

            Product product = new()
            {
                CategoryId = dto.CategoryId,
                VendorId = dto.VendorId,
                Description = dto.Description,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                Code = dto.Code,
                SlugUrl = dto.Name.AsSlug(),
                Name = dto.Name,
                CreatedDate = DateTime.UtcNow.AddHours(3),
            };

            await _unitOfWork.GetRepository<Product>().AddAsync(product);
            await _unitOfWork.GetRepository<Product>().SaveChangesAsync();

            return response;
        }

        public async Task<ResponseDto> Delete(int id)
        {
            ResponseDto response = new();

            var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);
            if (product == null)
                return response.Fail(Errors.ProductNotFound);

            _unitOfWork.GetRepository<Product>().Delete(product);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }

        public async Task<ResponseDto> Get(int id)
        {
            ResponseDto response = new();

            var product = await _unitOfWork.GetRepository<Product>().GetAsync(x => x.Id == id,
                                                                              x => x.Category,
                                                                              x => x.Vendor);

            if (product == null)
                return response.Fail(Errors.ProductNotFound);

            response.Data = new ProductResponseDto().Map(product);

            return response;
        }

        public async Task<ResponseDto> GetAll(PaginationFilter filter)
        {
            ResponseDto response = new();
            filter.PageNumber = filter.PageNumber > 1 ? filter.PageNumber : 1;
            filter.PageSize = filter.PageNumber > 10 ? filter.PageNumber : 10;

            var products = await _unitOfWork.GetRepository<Product>().PaginateAsync(filter,null);
            var data = products.Select(x => new ProductResponseDto().Map(x));

            response.Data = data;
            return response;
        }

        public async Task<ResponseDto> GetByUrl(string uri)
        {
            ResponseDto response = new();

            var product = await _unitOfWork.GetRepository<Product>().GetAsync(x => x.SlugUrl == uri);

            if (product == null)
                return response.Fail(Errors.ProductNotFound);

            response.Data = new ProductResponseDto().Map(product);

            return response;
        }

        public async Task<ResponseDto> GetByUrl(GetByVendorUrlRequestDto dto)
        {
            ResponseDto response = new();

            var vendor = await _unitOfWork.GetRepository<Vendor>().GetAsync(x => x.SlugUrl == dto.VendorUrl);
            if (vendor == null)
                return response.Fail(Errors.VendorNotFound);

            var product = await _unitOfWork.GetRepository<Product>().GetAsync(x => x.VendorId == vendor.Id && x.SlugUrl == dto.ProductUrl);
            if (product == null)
                return response.Fail(Errors.ProductNotFound);

            response.Data = new ProductResponseDto().Map(product);
            return response;
        }

        public async Task<ResponseDto> Update(UpdateProductDto dto)
        {
            ResponseDto response = new();

            var product = await _unitOfWork.GetRepository<Product>().GetAsync(x => x.Id == dto.Id);
            if (product == null)
                return response.Fail(Errors.ProductNotFound);

            var category = await _unitOfWork.GetRepository<Category>().GetAsync(x => x.Id == dto.CategoryId);
            if (category == null)
                return response.Fail(Errors.CategoryNotFound);

            product.Name = dto.Name;
            product.SlugUrl = dto.Name.AsSlug();
            product.Description = dto.Description;
            product.UnitPrice = dto.UnitPrice;
            product.Quantity = dto.Quantity;
            product.Code = dto.Code;
            product.UpdatedDate = DateTime.UtcNow.AddHours(3);

            _unitOfWork.GetRepository<Product>().Update(product);
            await _unitOfWork.GetRepository<Product>().SaveChangesAsync();

            response.Data = new ProductResponseDto().Map(product);
            return response;
        }
    }
}
