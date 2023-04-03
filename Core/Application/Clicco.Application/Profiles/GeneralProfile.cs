using AutoMapper;
using Clicco.Application.Features.Commands;
using Clicco.Application.Features.Commands.Reviews;
using Clicco.Domain.Core.Extensions;
using Clicco.Domain.Model;

namespace Clicco.Application.Profiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateAddressCommand, Address>();

            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(x => x.SlugUrl, opt => opt.MapFrom(z => z.Name.ToSeoFriendlyUrl()));

            CreateMap<CreateMenuCommand, Menu>();

            CreateMap<CreateProductCommand, Product>()
                .ForMember(x => x.SlugUrl, opt => opt.MapFrom(z => z.Name.ToSeoFriendlyUrl()));

            CreateMap<CreateReviewCommand, Review>();

            CreateMap<CreateTransactionCommand, Transaction>();

            CreateMap<CreateCouponCommand,Coupon>();
        }
    }
}
