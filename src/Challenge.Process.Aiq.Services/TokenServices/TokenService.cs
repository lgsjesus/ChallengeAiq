using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Optional;

namespace Challenge.Process.Aiq.Services.TokenServices;

public class TokenService(IConfiguration config) : ITokenService
{
    
    public async Task<Option<AuthorizationResponseDto>> LoginAsync(AuthorizationRequestDto dto)
    {
        if (string.Equals(dto.User, "admin") && string.Equals(dto.Password, "admin"))
            return Option.Some(AuthorizationResponseDto.Create(GenerateToken()));
        
        return Option.None<AuthorizationResponseDto>();
    }

    private  string GenerateToken()
    {
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];
        var expiry = DateTime.Now.AddMinutes(120);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: issuer,audience: audience,
            expires: expiry,signingCredentials: credentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}