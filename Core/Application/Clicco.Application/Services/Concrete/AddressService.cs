using Clicco.Application.Services.Abstract;
using Clicco.Domain.Model.Dtos.Address;

namespace Clicco.Application.Services.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        private readonly IUserSessionService _userSessionService;
        public AddressService(
            IUnitOfWork unitOfWork,
            ICacheManager cacheManager,
            IMapper mapper,
            IUserSessionService userSessionService)
        {
            _unitOfWork = unitOfWork;
            _cacheManager = cacheManager;
            _mapper = mapper;
            _userSessionService = userSessionService;
        }
        public async Task<ResponseDto> Create(CreateAddressDto dto)
        {
            var address = _mapper.Map<Address>(dto);
            await _unitOfWork.GetRepository<Address>().AddAsync(address);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseDto();
        }

        public async Task<ResponseDto> Delete(int id)
        {
            ResponseDto response = new();

            var address = await _unitOfWork.GetRepository<Address>().GetByIdAsync(id);
            if (address == null)
                return response.Fail(Errors.AddressNotFound);

            _unitOfWork.GetRepository<Address>().Delete(address);
            return response;
        }

        public async Task<ResponseDto> Get(int id)
        {
            ResponseDto response = new();

            var address = await _unitOfWork.GetRepository<Address>().GetAsync(x => x.Id == id);

            if (address == null)
                return response.Fail(Errors.CategoryNotFound);

            response.Data = address;

            return response;
        }

        public async Task<ResponseDto> GetMyAddresses()
        {
            ResponseDto response = new();
            int userId = _userSessionService.GetUserId();
            var data = _mapper.Map<List<AddressResponseDto>>(await _unitOfWork.GetRepository<Address>().GetManyAsync(x => x.UserId == userId));
            response.Data = data;
            return response;
        }

        public async Task<ResponseDto> Update(UpdateAddressDto dto)
        {
            ResponseDto response = new();
            var address = await _unitOfWork.GetRepository<Address>().GetByIdAsync(dto.Id);
            if (address == null)
                return response.Fail(Errors.AddressNotFound);

            address.Street = dto.Street;
            address.City = dto.City;
            address.State = dto.State;
            address.Country = dto.Country;
            address.ZipCode = dto.ZipCode;
            address.UserId = _userSessionService.GetUserId();

            _unitOfWork.GetRepository<Address>().Update(address);

            var cacheKey = string.Format(CacheKeys.Address, dto.Id);

            await _unitOfWork.SaveChangesAsync();
            await _cacheManager.SetAsync(cacheKey, address);

            response.Data = new AddressResponseDto().Map(address);

            return response;
        }
    }
}
