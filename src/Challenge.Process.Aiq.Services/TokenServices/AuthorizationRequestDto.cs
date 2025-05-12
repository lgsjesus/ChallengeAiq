using System.ComponentModel.DataAnnotations;

namespace Challenge.Process.Aiq.Services.TokenServices;

public sealed record AuthorizationRequestDto
{
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public required string EmailUser { get; set; }
    [MinLength(8)]
    public required string Password { get; set; }
}