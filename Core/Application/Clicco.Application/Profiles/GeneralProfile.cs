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
            //Delete commands really need to map? 

            CreateMap<CreateAddressCommand, Address>();
            CreateMap<DeleteAddressCommand, Address>();

            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(x => x.SlugUrl, opt => opt.MapFrom(z => z.Name.ToSeoFriendlyUrl()));
            CreateMap<DeleteCategoryCommand, Category>();

            CreateMap<CreateMenuCommand, Menu>();
            CreateMap<DeleteMenuCommand, Menu>();

            CreateMap<CreateProductCommand, Product>()
                .ForMember(x => x.SlugUrl, opt => opt.MapFrom(z => z.Name.ToSeoFriendlyUrl()));
            CreateMap<DeleteProductCommand, Product>();

            CreateMap<CreateReviewCommand, Review>();
            CreateMap<DeleteReviewCommand, Review>();

            CreateMap<CreateTransactionCommand, Transaction>();
            CreateMap<DeleteTransactionCommand, Transaction>();
        }
    }
}
