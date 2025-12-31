using WebApi.Services;
using WebApi.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IDeckService, InMemoryDeckService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Deck endpoints
// 2.4.1 - Create Deck
app.MapPost("/decks", async (Deck deck, IDeckService deckService) =>
{
    var createdDeck = await deckService.AddDeckAsync(deck);
    return Results.Created($"/decks/{createdDeck.Id}", createdDeck);
})
.WithName("CreateDeck")
.WithOpenApi();

// 2.4.2 - Add Card to Deck
app.MapPost("/decks/{deckId}/cards", async (int deckId, Card card, IDeckService deckService) =>
{
    var deck = await deckService.GetDeckByIdAsync(deckId);
    if (deck == null)
    {
        return Results.NotFound($"Deck with ID {deckId} not found");
    }
    
    deck.Cards.Add(card);
    await deckService.UpdateDeckAsync(deckId, deck);
    
    return Results.Ok(new { message = "Card added successfully", deck });
})
.WithName("AddCardToDeck")
.WithOpenApi();

// 2.4.3 - View all decks with optional filtering
app.MapGet("/decks", async (string? playerName, string? format, IDeckService deckService) =>
{
    var decks = await deckService.GetAllDecksAsync();
    
    // Apply filters
    if (!string.IsNullOrEmpty(playerName))
    {
        decks = decks.Where(d => d.PlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase));
    }
    
    if (!string.IsNullOrEmpty(format))
    {
        decks = decks.Where(d => d.Format.Equals(format, StringComparison.OrdinalIgnoreCase));
    }
    
    return Results.Ok(decks);
})
.WithName("GetAllDecks")
.WithOpenApi();

// 2.4.4 - Get cards from a specific deck
app.MapGet("/decks/{deckId}/cards", async (int deckId, IDeckService deckService) =>
{
    var deck = await deckService.GetDeckByIdAsync(deckId);
    if (deck == null)
    {
        return Results.NotFound($"Deck with ID {deckId} not found");
    }
    
    return Results.Ok(deck.Cards);
})
.WithName("GetDeckCards")
.WithOpenApi();

app.Run();
