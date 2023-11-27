using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Models.Response;
using Clicco.AuthAPI.Services.Abstract;
using Clicco.AuthServiceAPI.Data.Contracts;
using Clicco.AuthServiceAPI.Helpers;
using Clicco.AuthServiceAPI.Models;
using Clicco.AuthServiceAPI.Models.Dtos;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Shared.Models.Email;
using Microsoft.Extensions.Configuration;

namespace Clicco.AuthServiceAPI.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IResetCodeRepository _resetCodeRepository;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(IUserRepository userRepository,
            IEmailService emailService,
            IResetCodeRepository resetLinkRepository,
            ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _resetCodeRepository = resetLinkRepository;
            _tokenHandler = tokenHandler;
        }
        public async Task<ResponseDto> LoginAsync(string email, string password)
        {
            ResponseDto response = new();
            bool isSA = false;
            if (email.StartsWith('#'))
            {
                //Get original email address without # character
                email = email.Substring(1);
                isSA = true;
            }
            var user = await _userRepository.GetSingleAsync(m => m.Email == email);

            if (isSA && user != null && !user.IsSA)
            {
                return response.Fail(Errors.UnauthorizedOperation);
            }

            else if (user == null || user != null && !HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return response.Fail(Errors.IncorrectLogin);
            }

            var token = _tokenHandler.CreateAccessToken(user);

            response.Data = new LoginResponseDto(token, user.Email);

            return response;
        }

        public async Task<ResponseDto> RegisterAsync(RegisterDto dto, string password)
        {
            ResponseDto response = new();
            
            var exist = await UserExistsAsync(dto.Email);
            if(exist.IsSuccess)
            {
                return response.Fail(Errors.UserAlreadyExist);
            }
            
            User user = new()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                PhoneNumber = dto.PhoneNumber,
            };

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            await _emailService.SendRegistrationEmailAsync(new RegistrationEmailRequestDto
            {
                FullName = user.ToString(),
                To = user.Email
            });

            response.Data = new RegisteredUserDto()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender == true ? "Male" : "Female",
                PhoneNumber = user.PhoneNumber,
            };
            return response;
        }

        public async Task<ResponseDto> UserExistsAsync(string email)
        {
            ResponseDto response = new();
            var user = await _userRepository.GetSingleAsync(x => x.Email == email);
            if (user == null)
                return response.Fail(Errors.UserNotFound);
            return response;    
        }

        public async Task<ResponseDto> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            ResponseDto response = new();   
            var user = await _userRepository.GetSingleAsync(x => x.Email == dto.Email);
            if (user == null)
                return response.Fail(Errors.UserNotFound);
                        
            ResetCode resetCode = new()
            {
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow.AddHours(3),
                ExpirationDate = DateTime.UtcNow.AddHours(3).AddMinutes(10),
                Code = ResetCodeHelper.GenerateResetCode()
            };

            await _resetCodeRepository.AddAsync(resetCode);

            await _resetCodeRepository.SaveChangesAsync();

            await _emailService.SendForgotPasswordEmailAsync(new ForgotPasswordEmailRequestDto
            {
                FullName = user.ToString(),
                ResetCode = resetCode.Code,
                To = user.Email
            });
            

            return response;
        }

        public async Task<ResponseDto> ResetPasswordAsync(ResetPasswordDto dtoModel)
        {
            ResponseDto response = new();
            var resetCode = await _resetCodeRepository.GetSingleAsync(x => x.Code == dtoModel.ResetCode);
            if (!resetCode.IsValid())
                return response.Fail(Errors.InvalidResetCode);

            resetCode.IsActive = false;
            resetCode.UpdatedDate = DateTime.UtcNow.AddHours(3);
            await _resetCodeRepository.SaveChangesAsync();

            var user = await _userRepository.GetByIdAsync(resetCode.UserId);
            if (user == null)
                return response.Fail(Errors.UserNotFound);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(dtoModel.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.UpdatedDate = DateTime.UtcNow.AddHours(3);
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            await _emailService.SendResetPasswordEmailAsync(new ResetPasswordEmailRequestDto
            {
                To = user.Email,
                FullName = user.ToString(),
            });

            response.Message  = "Your password has been reset successfully!";
            return response;
        }
    }
}
