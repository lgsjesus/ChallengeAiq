using Optional;

namespace Challenge.Process.Aiq.Services.TokenServices;

public interface ITokenService
{
    Task<Option<AuthorizationResponseDto>> LoginAsync(AuthorizationRequestDto dto);
    Task<Option<AuthorizationResponseDto>> CreateNewUserAsync(AuthorizationRequestDto dto);
}