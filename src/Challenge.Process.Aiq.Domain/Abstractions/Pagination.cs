using System.ComponentModel.DataAnnotations;

namespace Challenge.Process.Aiq.Domain.Abstractions;

public sealed record Pagination
{
    [Required]
    public int  PageSize { get; set; } = 10;
    [Required]
    public int PageNumber { get; set; } = 1;
    [Required]
    [MinLength(2)]
    public string OrderBy { get; set; } = "Id";
}