﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Challenge.Process.Aiq.Domain.Abstractions;
using Challenge.Process.Aiq.Domain.Entities;
using Challenge.Process.Aiq.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Optional;

namespace Challenge.Process.Aiq.Services.TokenServices;

public class TokenService(IConfiguration config,IUserRepository userRepository) : ITokenService
{
    
    public async Task<Option<AuthorizationResponseDto>> LoginAsync(AuthorizationRequestDto dto)
    {
        var hasher = new PasswordHasher<User>();
        var user = await userRepository.GetUserByEmailAsync(dto.EmailUser);
        if(user == null)
            return Option.None<AuthorizationResponseDto>();
        
        var result = hasher.VerifyHashedPassword(user, user.Password, dto.Password);
       if (result == PasswordVerificationResult.Success)
        {
            return Option.Some(AuthorizationResponseDto.Create(GenerateToken(user.Email)));
        }
        return Option.None<AuthorizationResponseDto>();
            
    }

    public async Task<AuthorizationResponseDto> CreateNewUserAsync(AuthorizationRequestDto dto)
    {
        if (await userRepository.ExistsByEmailAsync(dto.EmailUser))
        {
            throw new UserException("Already exists user with this email");
        }
        
        var hasher = new PasswordHasher<User>();
        var userHarsher = User.Create(dto.EmailUser, dto.Password);
        var passwordHarsher = hasher.HashPassword(userHarsher, dto.Password);
        await userRepository.CreateUserAsync(dto.EmailUser, passwordHarsher);

        return AuthorizationResponseDto.Create(GenerateToken(dto.EmailUser));
    }

    private  string GenerateToken(in string emailUser)
    {
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];
        var expiry = DateTime.Now.AddMinutes(120);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, emailUser),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: issuer,audience: audience,
            claims: claims,
            expires: expiry,signingCredentials: credentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}