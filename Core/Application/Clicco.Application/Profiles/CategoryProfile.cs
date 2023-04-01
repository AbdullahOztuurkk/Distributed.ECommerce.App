﻿using AutoMapper;
using Clicco.Application.Features.Commands.Categories;
using Clicco.Domain.Model;

namespace Clicco.Application.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            CreateMap<DeleteCategoryCommand, Category>().ReverseMap();
        }
    }
}
