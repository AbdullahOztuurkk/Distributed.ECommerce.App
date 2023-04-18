using AutoMapper;
using Clicco.Application.Features.Commands;
using Clicco.Domain.Core.Extensions;
using Clicco.Domain.Model;

namespace Clicco.Application.Profiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateAddressCommand, Address>();
            CreateMap<UpdateAddressCommand, Address>();
            CreateMap<DeleteAddressCommand, Address>();

            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(x => x.SlugUrl, opt => opt.MapFrom(z => z.Name.ToSeoFriendlyUrl()));
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<DeleteCategoryCommand, Category>();

            CreateMap<CreateMenuCommand, Menu>();
            CreateMap<DeleteMenuCommand, Menu>();
            CreateMap<UpdateMenuCommand, Menu>();

            CreateMap<CreateProductCommand, Product>()
                .ForMember(x => x.SlugUrl, opt => opt.MapFrom(z => z.Name.ToSeoFriendlyUrl()));
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<DeleteProductCommand, Product>();

            CreateMap<CreateReviewCommand, Review>();
            CreateMap<UpdateReviewCommand, Review>();
            CreateMap<DeleteReviewCommand, Review>();

            CreateMap<CreateTransactionCommand, Transaction>();
            CreateMap<DeleteTransactionCommand, Transaction>();
            CreateMap<UpdateTransactionCommand, Transaction>();

            CreateMap<CreateCouponCommand, Coupon>();
            CreateMap<UpdateCouponCommand, Coupon>();
            CreateMap<DeleteCouponCommand, Coupon>();
        }
    }
}
