using System.Net.Mime;
using Challenge.Process.Aiq.Services.ProductServices;
using Challenge.Process.Aiq.WebApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge.Process.Aiq.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController(IProductService productService): ApiBaseController
{
    [HttpGet("Get/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(200, "Get Product.", typeof(ApiResponse<ProductDto>))]
    [SwaggerResponse(404, "Not Found Product.", typeof(ApiEmptyResponse))]
    [SwaggerResponse(401, "Not authorized.")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Get product by id.")]
    public async Task<IActionResult> GetProductById(int id)
        => HandleResponse(StatusCodes.Status200OK,await productService.GetProductById(id));
    
    [HttpPost("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(200, "Get list of Products.", typeof(ApiResponse<ProductDto[]>))]
    [SwaggerResponse(404, "Not Found Products.", typeof(ApiEmptyResponse))]
    [SwaggerResponse(401, "Not authorized.")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Get all products.")]
    public async Task<IActionResult> GetAllProducts()
        => HandleResponse(StatusCodes.Status200OK,await productService.GetProducts());
}