using AutoMapper;
using Clicco.Application.Features.Commands;
using Clicco.Domain.Model;

namespace Clicco.Application.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressCommand, Address>().ReverseMap();
            CreateMap<DeleteAddressCommand, Address>().ReverseMap();
        }
    }
}
