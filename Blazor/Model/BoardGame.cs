namespace Blazor.Model;

public class BoardGame
{
    public required string Owner { get; set;}
    public required string Genre { get; set;}
    public required string Title { get; set;}
    public required int PlayTime { get; set;}
    public required string FrontImage { get; set;}
}