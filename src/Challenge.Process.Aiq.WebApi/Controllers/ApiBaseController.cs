using Challenge.Process.Aiq.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Process.Aiq.WebApi.Controllers;

[Route("api/[controller]")]
public abstract class ApiBaseController : ControllerBase
{
    protected  IActionResult HandleResponse<T>(int statusCodes,T? retorno = null) where T : class 
    {
        try
        {
            switch (statusCodes)
            {
                case StatusCodes.Status201Created:
                    return Ok(ApiResponse<T>.CreateSuccess(retorno));
                case StatusCodes.Status404NotFound:
                    return NotFound(ApiEmptyResponse.CreateSuccess());
                default:
                    return Ok(ApiResponse<T>.CreateSuccess(retorno));
                
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<T>.CreateError(ex.Message));
        }
    }
    protected  async Task<IActionResult> HandleEmptyResponse(int statusCodes, Func<Task> action) 
    {
        try
        {
            await action.Invoke();
            switch (statusCodes)
            {
                case StatusCodes.Status204NoContent:
                    return NoContent();
                default:
                    return Ok(ApiEmptyResponse.CreateSuccess());
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ApiEmptyResponse.CreateError(ex.Message));
        }
    }
}