using SocioWeb.Entities.Dtos.AuthDto;

namespace SocioWeb.Infrastructure.Services.Auth;

public interface IAuthService
{
    Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto);
    Task<TokenResponseDto>    LoginAsync(LoginRequestDto dto);
    Task<TokenResponseDto>    VerifyEmailAsync(string token);
    Task                      LogoutAsync();
    Task<TokenResponseDto?>   GetMeAsync();
}
