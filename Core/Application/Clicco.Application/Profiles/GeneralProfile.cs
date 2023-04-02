using AutoMapper;
using Clicco.Application.Features.Commands;
using Clicco.Domain.Model;

namespace Clicco.Application.Profiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateAddressCommand, Address>();
            CreateMap<DeleteAddressCommand, Address>();

            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<DeleteCategoryCommand, Category>();

            CreateMap<CreateMenuCommand, Menu>();
            CreateMap<DeleteMenuCommand, Menu>();
        }
    }
}
