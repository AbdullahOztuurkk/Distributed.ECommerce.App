using CoreLib.CustomExtension;
using CoreLib.Entity.Enums;
using CoreLib.ResponseModel;
using IdentityService.API.Application.Services.Abstract;
using IdentityService.API.Domain.Concrete;
using IdentityService.API.Domain.Constant;
using IdentityService.API.Domain.Dtos;
using MassTransit;
using Shared.Constant;
using Shared.Event.Mail;
using Shared.Events.Mail.Base;
using System.Text;

namespace IdentityService.API.Application.Services.Concrete;

public class AuthService : BaseService, IAuthService
{
    private readonly ITokenService _tokenHandler;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public AuthService(ITokenService tokenHandler, ISendEndpointProvider sendEndpointProvider)
    {
        _tokenHandler = tokenHandler;
        this._sendEndpointProvider = sendEndpointProvider;
    }
    public async Task<BaseResponse> Login(LoginRequestDto request)
    {
        var response = new BaseResponse();
        var user = await Db.GetDefaultRepo<User>().GetAsync(x => x.Email == request.Email && x.Status == StatusType.ACTIVE);
        if (user == null)
        {
            return response.Fail(Error.U0000);
        }
        if (user.Status == StatusType.BLOCKED)
        {
            return response.Fail(Error.U0002);
        }
        if (!Hashing.ValidatePassword(user.PasswordHash, request.Password))
        {
            return response.Fail(Error.U0001);
        }

        var token = _tokenHandler.CreateAccessToken(user);

        response.Data = new LoginResponseDto(token, user.Email);

        return response;
    }

    public async Task<BaseResponse> Register(RegisterRequestDto request)
    {
        BaseResponse response = new();

        var existing = await Db.GetDefaultRepo<User>().GetAsync(x => x.Email == request.Email && x.Status == StatusType.ACTIVE);
        if (existing == null)
            return response.Fail(Error.U0000);

        User user = new()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
        };

        user.PasswordHash = Encoding.UTF8.GetBytes(Hashing.HashPassword(request.Password));

        await Db.GetDefaultRepo<User>().InsertAsync(user);
        await Db.GetDefaultRepo<User>().SaveChanges();
        Db.Commit();

        var emailEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.RegistrationEmailRequestQueue}"));

        await emailEndpoint.Send<EmailRequestEvent>(new RegistrationEmailRequestEvent
        {
            FullName = user.FullName,
            To = request.Email
        });

        return response;
    }

    public async Task<BaseResponse> ForgotPassword(ForgotPasswordRequestDto dto)
    {
        BaseResponse response = new();
        var existUser = await Db.GetDefaultRepo<User>().GetAsync(x => x.Email == dto.Email && x.Status == StatusType.ACTIVE);
        if (existUser == null)
            return response.Fail(Error.U0000);

        ResetCode resetCode = new()
        {
            UserId = existUser.Id,
            ExpirationDate = DateTime.UtcNow.AddHours(3).AddMinutes(10),
            Code = GenerateResetCode()
        };

        await Db.GetDefaultRepo<ResetCode>().InsertAsync(resetCode);
        await Db.GetDefaultRepo<ResetCode>().SaveChanges();
        Db.Commit();

        var emailEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.ForgotPasswordEmailRequestQueue}"));

        await emailEndpoint.Send<EmailRequestEvent>(new ForgotPasswordEmailRequestEvent
        {
            ResetCode = resetCode.Code,
            FullName = existUser.FullName,
            To = existUser.Email
        });

        return response;
    }

    public async Task<BaseResponse> ResetPassword(ResetPasswordRequestDto request)
    {
        var response = new BaseResponse();
        var resetCode = await Db.GetDefaultRepo<ResetCode>().GetAsync(x => x.Code == request.Code && x.Status == StatusType.ACTIVE);
        if (resetCode == null)
        {
            return response.Fail(Error.U0000);
        }
        else if (resetCode.ExpirationDate <= DateTime.UtcNow.AddHours(3))
        {
            return response.Fail(Error.U0003);
        }

        resetCode.IsActive = false;
        await Db.GetDefaultRepo<ResetCode>().SaveChanges();

        var user = await Db.GetDefaultRepo<User>().GetByIdAsync(resetCode.UserId);
        if (user == null)
        {
            return response.Fail(Error.U0000);
        }

        user.PasswordHash = Encoding.UTF8.GetBytes(Hashing.HashPassword(request.Password));

        await Db.GetDefaultRepo<User>().SaveChanges();
        Db.Commit();

        var emailEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueNames.ResetPasswordEmailRequestQueue}"));

        await emailEndpoint.Send<EmailRequestEvent>(new ResetPasswordEmailRequestEvent
        {
            FullName = user.FullName,
            To = user.Email,
        });

        return response;
    }

    private static string GenerateResetCode(int length = 12)
    {
        const string Characters = "0123456789";
        StringBuilder sb = new StringBuilder();
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(Characters.Length);
            sb.Append(Characters[index]);
        }

        return sb.ToString();
    }
}
