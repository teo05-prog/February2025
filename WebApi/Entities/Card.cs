namespace WebApi.Entities;

public class Card
{
    public int Id { get; set; }
    public required string CardName { get; set; }
    public required int ManaCost { get; set; }
    public required Boolean IsCreature { get; set; }
}