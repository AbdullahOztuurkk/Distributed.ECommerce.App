using Clicco.Domain.Model.Dtos.Address;

namespace Clicco.Application.Profiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateAddressDto, Address>();
            CreateMap<UpdateAddressDto, Address>();
            CreateMap<DeleteAddressDto, Address>();

            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(x => x.SlugUrl, opt => opt.MapFrom(z => z.Name.AsSlug()));
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<DeleteCategoryCommand, Category>();

            CreateMap<CreateMenuCommand, Menu>();
            CreateMap<DeleteMenuCommand, Menu>();
            CreateMap<UpdateMenuCommand, Menu>();

            CreateMap<CreateProductCommand, Product>()
                .ForMember(x => x.SlugUrl, opt => opt.MapFrom(z => z.Name.AsSlug()));
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<DeleteProductCommand, Product>();

            CreateMap<CreateReviewCommand, Review>();
            CreateMap<UpdateReviewCommand, Review>();
            CreateMap<DeleteReviewCommand, Review>();

            //CreateMap<CreateTransactionCommand, Transaction>();
            CreateMap<DeleteTransactionCommand, Transaction>();
            CreateMap<UpdateTransactionCommand, Transaction>();

            CreateMap<CreateCouponCommand, Coupon>();
            CreateMap<UpdateCouponCommand, Coupon>();
            CreateMap<DeleteCouponCommand, Coupon>();

            CreateMap<CreateVendorCommand, Vendor>();
            CreateMap<UpdateVendorCommand, Vendor>();
            CreateMap<DeleteVendorCommand, Vendor>();
        }
    }
}
