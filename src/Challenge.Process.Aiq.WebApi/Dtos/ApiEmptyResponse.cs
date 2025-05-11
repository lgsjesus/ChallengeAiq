namespace Challenge.Process.Aiq.WebApi.Dtos;

public sealed class ApiEmptyResponse
{
    public string? Error { get; set; }
    public bool Success {get; set;}
    public object? Data { get; set; }
    
    public static ApiEmptyResponse CreateSuccess() => new()
    {
        Success = true,
        Error = null,
    };
    public static ApiEmptyResponse CreateError(in string error) => new()
    {
        Success = false,
        Error = error,
    };
}