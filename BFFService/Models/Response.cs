namespace BFFService.Models;
public record Response
{
    public object? Content { get; init; }

    public string? Messagem { get; set; }
}