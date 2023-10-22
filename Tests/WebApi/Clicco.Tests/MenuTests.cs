using AutoMapper;
using Clicco.Application.Features.Commands;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core.Extensions;
using Clicco.Domain.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Clicco.Persistence.Tests
{
    public class MenuTests
    {
        private Mock<IMenuRepository> mockMenuRepository;
        private Mock<IMenuService> mockMenuService;
        private Mock<IMapper> mockMapper;

        [OneTimeSetUp]
        public void Setup()
        {
            mockMapper = new Mock<IMapper>();
            mockMenuRepository = new Mock<IMenuRepository>();
            mockMenuService = new Mock<IMenuService>();
        }


        [Test]
        public async Task AddMenu_WhenItsAddedToSubCategory_MustBeCorrectUrl()
        {
            Category ParentCategory = new()
            {
                Id = 1,
                SlugUrl = "parent-category-url"
            };

            Category SubCategory = new()
            {
                Id = 2,
                ParentId = ParentCategory.Id,
                SlugUrl = "sub-category-url"
            };

            mockMenuService.Setup(x => x.CheckCategoryId(It.IsAny<int>())).Returns(Task.CompletedTask);
            mockMenuService.Setup(x => x.CheckSlugUrl(It.IsAny<string>())).Returns(Task.CompletedTask);

            mockMenuRepository.Setup(x => x.GetExactSlugUrlByCategoryId(It.IsAny<int>())).Returns(() =>
            {
                List<string> urls = new() { SubCategory.SlugUrl, ParentCategory.SlugUrl };
                urls.Reverse();
                return urls.ConcatUrls();
            });

            //CreateMenuCommandHandler handler = new(
            //    mapper: mockMapper.Object ,
            //    menuRepository: mockMenuRepository.Object,
            //    menuService: mockMenuService.Object);

            CreateMenuCommand createMenuCommand = new()
            {
                CategoryId = SubCategory.Id,
            };
            
            Menu menu = new()
            {
                CategoryId = createMenuCommand.CategoryId,
                Category = SubCategory,
                SlugUrl = mockMenuRepository.Object.GetExactSlugUrlByCategoryId(createMenuCommand.CategoryId)
            };

            mockMapper.Setup(x => x.Map<Menu>(createMenuCommand)).Returns(menu);
            
            //await handler.Handle(createMenuCommand,new CancellationToken());

            menu.SlugUrl.Should().Be("parent-category-url-sub-category-url");
        }

    }
}
