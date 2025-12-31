using WebApi.Entities;

namespace WebApi.Services;

public class InMemoryDeckService : IDeckService
{
    private List<Deck> decks;
    private int nextId;
    
    public InMemoryDeckService()
    {
        // Initialize dummy data in the constructor
        decks = new()
        {
            new Deck
            {
                Id = 1,
                PlayerName = "Alice",
                Format = "Standard",
                Cards = new List<Card>
                {
                    new Card
                    {
                        Id = 1,
                        CardName = "Card A",
                        ManaCost = 2,
                        IsCreature = true
                    },
                    new Card
                    {
                        Id = 2,
                        CardName = "Card B",
                        ManaCost = 3,
                        IsCreature = false
                    }
                }
            },
            new Deck
            {
                Id = 2,
                PlayerName = "Bob",
                Format = "Modern",
                Cards = new List<Card>
                {
                    new Card
                    {
                        Id = 3,
                        CardName = "Card C",
                        ManaCost = 1,
                        IsCreature = true
                    },
                    new Card
                    {
                        Id = 4,
                        CardName = "Card D",
                        ManaCost = 4,
                        IsCreature = false
                    }
                }
            }
        };
        nextId = 3;
    }


    public async Task<Deck> AddDeckAsync(Deck deck)
    {
        deck.Id = nextId++;
        decks.Add(deck);
        return await Task.FromResult(deck);
    }

    public async Task<Deck?> GetDeckByIdAsync(int id)
    {
        var deck = decks.FirstOrDefault(d => d.Id == id);
        return await Task.FromResult(deck);
    }

    public async Task<IEnumerable<Deck>> GetAllDecksAsync()
    {
        return await Task.FromResult(decks.AsEnumerable());
    }

    public async Task UpdateDeckAsync(int id, Deck deck)
    {
        var index = decks.FindIndex(d => d.Id == id);
        if (index != -1)
        {
            decks[index] = deck;
        }
        await Task.CompletedTask;
    }
}