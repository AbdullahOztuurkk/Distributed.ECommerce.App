using AutoMapper;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;

namespace Clicco.Application.Profiles
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<Address, AddressViewModel>();

            CreateMap<Category,CategoryViewModel>();

            CreateMap<Coupon, CouponViewModel>()
                .ForMember(x => x.DiscountType, opt => opt.MapFrom(o => Enum.ToObject(typeof(DiscountType), (int)o.DiscountType)))
                .ForMember(x => x.Type, opt => opt.MapFrom(o => Enum.ToObject(typeof(CouponType), (int)o.Type)));

            CreateMap<Menu, MenuViewModel>();

            CreateMap<Transaction, TransactionViewModel>();

            CreateMap<Vendor,VendorViewModel>();

            CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(o => o.Category.Name))
                .ForMember(x => x.VendorName, opt => opt.MapFrom(o => o.Vendor.Name));

            CreateMap<Review, ReviewViewModel>()
                .ForMember(x => x.ProductUrl, opt => opt.MapFrom(o => o.Product.SlugUrl));
        }
    }
}
