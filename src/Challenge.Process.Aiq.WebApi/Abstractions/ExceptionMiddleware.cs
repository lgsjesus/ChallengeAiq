using Challenge.Process.Aiq.WebApi.Dtos;
using System.Text.Json;
using System.Text;
using Challenge.Process.Aiq.Domain.Abstractions;

namespace Challenge.Process.Aiq.WebApi.Abstractions
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (UserException ex)
            {
                await ConfigureReturn(httpContext, ex, StatusCodes.Status400BadRequest);
            }
            catch (UserNotFoundException ex)
            {
                await ConfigureReturn(httpContext, ex, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                await ConfigureReturn(httpContext, ex, StatusCodes.Status500InternalServerError);
            }
        }

        private async Task ConfigureReturn(HttpContext httpContext, Exception ex,int statusCode)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            var erro = JsonSerializer.Serialize(ApiResponse<UserException>.CreateError(ex.Message),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = new LowerCasePolicy(),
                    WriteIndented = true
                });
            var buffer = Encoding.UTF8.GetBytes(erro);
            await httpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
