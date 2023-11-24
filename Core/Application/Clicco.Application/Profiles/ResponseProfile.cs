using Clicco.Domain.Model.Dtos.Address;
using Clicco.Domain.Model.Dtos.Category;
using Clicco.Domain.Model.Dtos.Coupon;
using Clicco.Domain.Model.Dtos.Menu;
using Clicco.Domain.Model.Dtos.Transaction;

namespace Clicco.Application.Profiles
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<Address, AddressResponseDto>();

            CreateMap<Category, CategoryResponseDto>();

            CreateMap<Coupon, CouponResponseDto>();

            CreateMap<Menu, MenuResponseDto>();

            CreateMap<Transaction, TransactionResponseDto>();

            CreateMap<Vendor, VendorResponseDto>();

            CreateMap<Product, ProductResponseDto>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(o => o.Category.Name))
                .ForMember(x => x.VendorName, opt => opt.MapFrom(o => o.Vendor.Name));

            CreateMap<Review, ReviewResponseDto>()
                .ForMember(x => x.ProductUrl, opt => opt.MapFrom(o => o.Product.SlugUrl));
        }
    }
}
