using Clicco.Application.Services.Abstract;
using Clicco.Domain.Model.Dtos.Category;

namespace Clicco.Application.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        public CategoryService(
            IUnitOfWork unitOfWork,
            ICacheManager cacheManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cacheManager = cacheManager;
            _mapper = mapper;
        }
        public async Task<ResponseDto> Create(CreateCategoryDto dto)
        {
            ResponseDto response = new();
            if (dto.ParentId.HasValue)
            {
                var parent = await _unitOfWork.GetRepository<Category>().GetByIdAsync(dto.ParentId.Value);
                if (parent == null)
                    return response.Fail(Errors.CategoryNotFound);
            }
            if (dto.MenuId.HasValue)
            {
                var menu = await _unitOfWork.GetRepository<Menu>().GetByIdAsync(dto.MenuId.Value);
                if (menu == null)
                    return response.Fail(Errors.MenuNotFound);
            }

            var category = _mapper.Map<Category>(dto);
            await _unitOfWork.GetRepository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            await _cacheManager.RemoveAsync(CacheKeys.GetListKey<CategoryResponseDto>());

            return response;
        }

        public async Task<ResponseDto> Delete(int id)
        {
            ResponseDto response = new();
            var cacheKey = string.Format(CacheKeys.Category, id);

            var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(id);
            if (category == null)
                return response.Fail(Errors.CategoryNotFound);

            _unitOfWork.GetRepository<Category>().Delete(category);
            //var categoryMenus = (List<Menu>)await _unitOfWork.GetRepository<Menu>().GetManyAsync(x => x.CategoryId == category.Id && x.IsActive);

            //await categoryService.DisableMenuId(request.Id);
            await _unitOfWork.SaveChangesAsync();
            await _cacheManager.RemoveAsync(cacheKey);

            return response;
        }

        public async Task<ResponseDto> Get(int id)
        {
            ResponseDto response = new();

            var category = await _unitOfWork.GetRepository<Category>().GetAsync(x => x.Id == id);

            if (category == null)
                return response.Fail(Errors.CategoryNotFound);

            response.Data = category;

            return response;
        }

        public async Task<ResponseDto> GetAll(PaginationFilter filter)
        {
            ResponseDto response = new();
            filter.PageNumber = filter.PageNumber > 1 ? filter.PageNumber : 1;
            filter.PageSize = filter.PageNumber > 10 ? filter.PageNumber : 10;

            var categories = await _unitOfWork.GetRepository<Category>().PaginateAsync(filter,null);
            response.Data = categories;
            return response;
        }

        public async Task<ResponseDto> GetByUrl(string url)
        {
            ResponseDto response = new();

            var category = await _unitOfWork.GetRepository<Category>().GetAsync(x => x.SlugUrl == url);

            if (category == null)
                return response.Fail(Errors.CategoryNotFound);

            response.Data = category;

            return response;
        }

        public async Task<ResponseDto> Update(UpdateCategoryDto dto)
        {
            ResponseDto response = new();
            var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(dto.Id);
            if (category == null)
                return response.Fail(Errors.CategoryNotFound);

            if (dto.ParentId.HasValue)
            {
                var parent = await _unitOfWork.GetRepository<Category>().GetByIdAsync(dto.ParentId.Value);
                if (parent == null)
                    return response.Fail(Errors.CategoryNotFound);
            }
            if (dto.MenuId.HasValue)
            {
                var menu = await _unitOfWork.GetRepository<Menu>().GetByIdAsync(dto.MenuId.Value);
                if (menu == null)
                    return response.Fail(Errors.MenuNotFound);
            }

            var isExist = await GetByUrl(dto.Name.AsSlug());
            if (isExist.IsSuccess)
            {
                return response.Fail(Errors.CategoryAlreadyExist);
            }

            category.Name = dto.Name;
            category.SlugUrl = dto.Name.AsSlug();

            await _unitOfWork.SaveChangesAsync();
            await _cacheManager.RemoveAsync(CacheKeys.GetListKey<Category>());

            response.Data = new CategoryResponseDto().Map(category);
            return response;
        }
    }
}
