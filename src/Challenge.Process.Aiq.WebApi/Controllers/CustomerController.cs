using System.Net.Mime;
using Challenge.Process.Aiq.Domain.Abstractions;
using Challenge.Process.Aiq.Services.CustomerServices;
using Challenge.Process.Aiq.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Challenge.Process.Aiq.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(ICustomerService customerService) : ApiBaseController
{
    [HttpGet("Get/{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(200, "Found Customer.", typeof(ApiResponse<CustomerDto>))]
    [SwaggerResponse(404, "Not found Customer.", typeof(ApiEmptyResponse))]
    [SwaggerOperation(Summary = "Get customer by id.")]
    public async Task<IActionResult> GetCustomerById(long id)
        => HandleResponse(StatusCodes.Status200OK,await customerService.GetByIdAsync(id));
    
    [HttpPost("Create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(201, "Created Customer.", typeof(ApiResponse<CustomerDto>))]
    [SwaggerResponse(400, "Erro create Customer.", typeof(ApiEmptyResponse))]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Create new customer.")]
    public async Task<IActionResult> CreateCustomer( CreateCustomerDto dto)
        => HandleResponse(StatusCodes.Status201Created,await customerService.CreateAsync(dto));
    
    [HttpPost("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(200, "List of found Customer.", typeof(ApiResponse<IEnumerable<CustomerDto>>))]
    [SwaggerResponse(404, "Not found Customer.", typeof(ApiEmptyResponse))]
    [SwaggerOperation(Summary = "Get all customers.")]
    public async Task<IActionResult> GetAllCustomers([FromBody] Pagination dto)
        => HandleResponse(StatusCodes.Status200OK,await customerService.GetAllAsync(dto));
    
    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(200, "Updated Customer.", typeof(ApiResponse<CustomerDto>))]
    [SwaggerResponse(404, "Not found Customer to Update.", typeof(ApiEmptyResponse))]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Update customer.")]
    public async Task<IActionResult> UpdateCustomer( CustomerDto dto)
        => HandleResponse(StatusCodes.Status200OK,await customerService.UpdateAsync(dto));

    [HttpDelete("Delete/{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(204, "Deleted Customer.")]
    [SwaggerResponse(404, "Not found Customer to delete.", typeof(ApiEmptyResponse))]
    [SwaggerOperation(Summary = "Delete customer and your favorites products.")]
    public async Task<IActionResult> DeleteCustomer(long id)
    {
        return await HandleEmptyResponse( StatusCodes.Status204NoContent,async ()=> await customerService.RemoveByIdAsync(id));
    }
    
}