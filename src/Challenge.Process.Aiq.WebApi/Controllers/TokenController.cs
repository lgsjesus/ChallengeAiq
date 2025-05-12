using System.Net.Mime;
using Challenge.Process.Aiq.Services.TokenServices;
using Challenge.Process.Aiq.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Optional.Unsafe;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge.Process.Aiq.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class TokenController(ITokenService tokenService) : ApiBaseController
{
    [HttpPost("Authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "Get Authorization Token Access.", typeof(ApiResponse<AuthorizationResponseDto>))]
    [SwaggerResponse(401, "Not authorized.", typeof(ApiEmptyResponse))]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Generate token to access Api.")]
    public async Task<IActionResult> Authenticate(AuthorizationRequestDto dto)
    {
        var result = await tokenService.LoginAsync(dto);
        if (result.HasValue)
            return HandleResponse(StatusCodes.Status200OK, result.ValueOrDefault());
        else
           return await HandleEmptyResponse(StatusCodes.Status401Unauthorized);
    }
    [HttpPost("CreateUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(200, "Get Authorization Token Access.", typeof(ApiResponse<AuthorizationResponseDto>))]
    [SwaggerResponse(404, "Not authorized.", typeof(ApiEmptyResponse))]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Create user and return token to access Api.")]
    public async Task<IActionResult> CreateUser(AuthorizationRequestDto dto)
    {
        return HandleResponse(StatusCodes.Status200OK, await tokenService.CreateNewUserAsync(dto));
    }

}