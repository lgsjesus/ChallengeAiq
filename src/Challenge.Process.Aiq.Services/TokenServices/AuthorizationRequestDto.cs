namespace Challenge.Process.Aiq.Services.TokenServices;

public sealed record AuthorizationRequestDto
{
    public required string User { get; set; }
    public required string Password { get; set; }
}