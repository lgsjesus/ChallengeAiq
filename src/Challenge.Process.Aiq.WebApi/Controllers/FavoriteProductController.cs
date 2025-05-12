using System.Net.Mime;
using Challenge.Process.Aiq.Services.FavoriteProductServices;
using Challenge.Process.Aiq.WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge.Process.Aiq.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FavoriteProductController(IFavoriteProductService favoriteProductService): ApiBaseController
{
    [HttpGet("Get/{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(200, "Found Favorite Products from Customer.", typeof(ApiResponse<FavoriteProductsDto>))]
    [SwaggerResponse(404, "Not Found Favorite Products from Customer.", typeof(ApiEmptyResponse))]
    [SwaggerResponse(401, "Not authorized.")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Get all favorites products for customer.")]
    public async Task<IActionResult> GetFavoritesProductsFromCustomer(long customerId)
        => HandleResponse(StatusCodes.Status200OK,await favoriteProductService.GetAllFavoriteProductsByCustomerIdAsync(customerId));

    [HttpPost("Create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(201, "Create Favorite Products to Customer.", typeof(ApiEmptyResponse))]
    [SwaggerResponse(404, "Not Found Favorite Products from Customer.", typeof(ApiEmptyResponse))]
    [SwaggerResponse(401, "Not authorized.")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Create a favorite product for customer.")]
    public async Task<IActionResult> CreateFavoriteProductToCustomer(
      [FromBody]  CreateFavoriteProductToCustomerDto favoriteProductsDto)
    {
       return await HandleEmptyResponse(StatusCodes.Status200OK,async ()=>  await favoriteProductService.CreateFavoriteProductToCustomerAsync(favoriteProductsDto));
    }
    [HttpDelete("Delete")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(204, "Deleted  Favorite Products from Customer.")]
    [SwaggerResponse(404, "Not found Favorite Products to delete.", typeof(ApiEmptyResponse))]
    [SwaggerResponse(401, "Not authorized.")]
    [SwaggerOperation(Summary = "Delete Favorite Products from Customer")]
    public async Task<IActionResult> DeleteCustomer([FromBody] RemoveFavoriteProductToCustomerDto dto)
    {
        return await HandleEmptyResponse( StatusCodes.Status204NoContent,async ()=> 
            await favoriteProductService.RemoveFavoriteProductFromCustomer(dto));
    }
}