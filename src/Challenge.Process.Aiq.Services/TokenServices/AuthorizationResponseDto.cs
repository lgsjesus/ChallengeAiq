namespace Challenge.Process.Aiq.Services.TokenServices;

public sealed record AuthorizationResponseDto
{
    public required string Token { get; set; }

    public static AuthorizationResponseDto Create(in string accessToken) => new()
    {
        Token = accessToken
    };
}