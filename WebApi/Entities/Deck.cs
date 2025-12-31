using System.Collections.ObjectModel;

namespace WebApi.Entities;

public class Deck
{
    public int Id { get; set; }
    public required string PlayerName { get; set; }
    public required string Format { get; set; }
    public required List<Card> Cards { get; set; }
}