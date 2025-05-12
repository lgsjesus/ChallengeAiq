namespace Challenge.Process.Aiq.Services.TokenServices;

public sealed record AuthorizationResponseDto
{
    public required string AccessToken { get; set; }

    public static AuthorizationResponseDto Create(in string accessToken) => new()
    {
        AccessToken = accessToken
    };
}