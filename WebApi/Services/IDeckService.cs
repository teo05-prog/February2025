using WebApi.Entities;

namespace WebApi.Services;

public interface IDeckService
{
    Task<Deck> AddDeckAsync(Deck deck);
    Task<Deck?> GetDeckByIdAsync(int id);
    Task<IEnumerable<Deck>> GetAllDecksAsync();
    Task UpdateDeckAsync(int id, Deck deck);
}