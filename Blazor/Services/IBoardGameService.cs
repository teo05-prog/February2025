using Blazor.Model;

namespace Blazor.Services;

public interface IBoardGameService
{
    Task<BoardGame> AddBoardGameAsync(BoardGame boardGame);
    Task<BoardGame?> GetBoardGameByIdAsync(int id);
    Task<IEnumerable<BoardGame>> GetAllBoardGamesAsync();
    Task UpdateBoardGameAsync(int id, BoardGame boardGame);
    Task DeleteBoardGameAsync(int id);
}