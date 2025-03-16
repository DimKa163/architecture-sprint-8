namespace Api.Contracts;

public record Report
{
    public string Title { get; init; }
    
    public DateTime Begin { get; init; }
    
    public DateTime End { get; init; }
}