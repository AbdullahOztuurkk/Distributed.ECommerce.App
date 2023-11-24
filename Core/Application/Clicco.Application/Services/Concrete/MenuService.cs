﻿using Clicco.Application.Services.Abstract;
using Clicco.Domain.Model.Dtos.Menu;

namespace Clicco.Application.Services.Concrete
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuRepository _menuRepository;
        private readonly ICacheManager _cacheManager;
        public MenuService(
            IUnitOfWork unitOfWork,
            ICacheManager cacheManager,
            IMenuRepository menuRepository)
        {
            _unitOfWork = unitOfWork;
            _cacheManager = cacheManager;
            _menuRepository = menuRepository;
        }
        public async Task<ResponseDto> Create(CreateMenuDto dto)
        {
            ResponseDto response = new();

            var category = await _unitOfWork.GetRepository<Category>().GetAsync(x => x.Id == dto.CategoryId);
            if (category == null)
                return response.Fail(Errors.CategoryNotFound);

            var exactUri = _menuRepository.GetExactSlugUrlByCategoryId(dto.CategoryId);
            var isExist = await _menuRepository.GetAsync(x => x.SlugUrl == exactUri);
            if (isExist != null)
                return response.Fail(Errors.MenuAlreadyExist);

            Menu menu = new()
            {
                CategoryId = dto.CategoryId,
                SlugUrl = exactUri,
                IsActive = true,
                Name = dto.Name,
            };

            await _menuRepository.AddAsync(menu);
            await _menuRepository.SaveChangesAsync();

            return response;
        }

        public async Task<ResponseDto> Delete(int id)
        {
            ResponseDto response = new();

            var address = await _unitOfWork.GetRepository<Coupon>().GetByIdAsync(id);
            if (address == null)
                return response.Fail(Errors.AddressNotFound);

            _unitOfWork.GetRepository<Coupon>().Delete(address);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }

        public Task<ResponseDto> DeleteByCategoryId(int id)
        {
            return Task.FromResult(new ResponseDto());
            //TODO: IMPLEMENT FUNC
        }

        public async Task<ResponseDto> Get(int id)
        {
            ResponseDto response = new();

            var menu = await _unitOfWork.GetRepository<Menu>().GetAsync(x => x.Id == id);

            if (menu == null)
                return response.Fail(Errors.MenuNotFound);

            response.Data = menu;

            return response;
        }

        public async Task<ResponseDto> GetAll(PaginationFilter filter)
        {
            ResponseDto response = new();
            filter.PageNumber = filter.PageNumber > 1 ? filter.PageNumber : 1;
            filter.PageSize = filter.PageNumber > 10 ? filter.PageNumber : 10;

            var menus = await _unitOfWork.GetRepository<Menu>().PaginateAsync(filter,null);
            response.Data = menus;
            return response;
        }

        public async Task<ResponseDto> GetByUrl(string uri)
        {
            ResponseDto response = new();

            var menu = await _unitOfWork.GetRepository<Menu>().GetAsync(x => x.SlugUrl == uri);

            if (menu == null)
                return response.Fail(Errors.MenuNotFound);

            response.Data = menu;

            return response;
        }

        public async Task<ResponseDto> Update(UpdateMenuDto dto)
        {
            ResponseDto response = new();

            var menu = await _unitOfWork.GetRepository<Menu>().GetAsync(x => x.Id == dto.Id);
            if (menu == null)
                return response.Fail(Errors.MenuNotFound);

            var category = await _unitOfWork.GetRepository<Category>().GetAsync(x => x.Id == dto.CategoryId);
            if (category == null)
                return response.Fail(Errors.CategoryNotFound);

            var exactUri = _menuRepository.GetExactSlugUrlByCategoryId(dto.CategoryId);
            var isExist = await _menuRepository.GetAsync(x => x.SlugUrl == exactUri);
            if (isExist != null)
                return response.Fail(Errors.MenuAlreadyExist);

            menu.Name = dto.Name;
            menu.SlugUrl = exactUri;
            menu.CategoryId = dto.CategoryId;

            _menuRepository.Update(menu);
            await _menuRepository.SaveChangesAsync();

            response.Data = new MenuResponseDto().Map(menu);
            return response;
        }
    }
}
