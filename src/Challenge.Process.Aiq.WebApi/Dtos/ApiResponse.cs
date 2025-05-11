namespace Challenge.Process.Aiq.WebApi.Dtos;

public sealed record ApiResponse<T>  where T : class 
{
    public string? Error { get; set; }
    public bool Success {get; set;}
    public T? Data { get; set; }
    
    public static ApiResponse<T> CreateSuccess(in T? data) => new()
    {
        Data = data,
        Success = true,
        Error = null,
    };
    public static ApiResponse<T> CreateError( in string? error) => new()
    {
        Data = null,
        Success = false,
        Error = error,
    };
}