namespace Challenge.Process.Aiq.Domain.Abstractions;

public sealed record Pagination
{
    public int  PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public string OrderBy { get; set; } = "Id";
}