using Blazor.Model;

namespace Blazor.Services;

public class InMemoryBoardGameService : IBoardGameService
{
    private List<BoardGame> boardGames;

    public InMemoryBoardGameService()
    {
        // Initialize dummy data in the constructor
        boardGames = new()
        {
            new BoardGame
            {
                Title = "Azul",
                Owner = "Michael Kiesling",
                Genre = "Abstract",
                PlayTime = 30,
                FrontImage = "https://store.asmodee.com/cdn/shop/products/NM6010-1_535x.jpg?v=1690217148"
            },
            new BoardGame
            {
                Title = "Carcassonne",
                Owner = "Klaus-Jürgen Wrede",
                Genre = "Strategy",
                PlayTime = 40,
                FrontImage = "https://cdn.svc.asmodee.net/production-zman/uploads/image-converter/2024/08/ZM7810_box-right.webp"
            },
            new BoardGame
            {
                Title = "Catan",
                Owner = "Klaus Teuber",
                Genre = "Strategy",
                PlayTime = 60,
                FrontImage = "https://www.asmodee.co.uk/cdn/shop/files/CN3081_1.jpg?v=1765348079&width=1214"
            },
            new BoardGame
            {
                Title = "Pandemic",
                Owner = "Matt Leacock",
                Genre = "Cooperative",
                PlayTime = 45,
                FrontImage = "https://cdn.svc.asmodee.net/production-zman/uploads/image-converter/2024/08/ZM7101-1_720x.webp"
            },
            new BoardGame
            {
                Title = "Ticket to Ride",
                Owner = "Alan R. Moon",
                Genre = "Adventure",
                PlayTime = 45,
                FrontImage = "https://x.boardgamearena.net/data/gamemedia/tickettoride/box/en_280.png?h=1651658908"
            }
        };
    }

    public async Task<BoardGame> AddBoardGameAsync(BoardGame boardGame)
    {
        boardGames.Add(boardGame);
        return await Task.FromResult(boardGame);
    }

    public async Task<BoardGame?> GetBoardGameByIdAsync(int id)
    {
        var boardGame = boardGames.ElementAtOrDefault(id);
        return await Task.FromResult(boardGame);
    }

    public async Task<IEnumerable<BoardGame>> GetAllBoardGamesAsync()
    {
        return await Task.FromResult(boardGames.AsEnumerable());
    }

    public async Task UpdateBoardGameAsync(int id, BoardGame boardGame)
    {
        if (id >= 0 && id < boardGames.Count)
        {
            boardGames[id] = boardGame;
        }
        await Task.CompletedTask;
    }

    public async Task DeleteBoardGameAsync(int id)
    {
        if (id >= 0 && id < boardGames.Count)
        {
            boardGames.RemoveAt(id);
        }
        await Task.CompletedTask;
    }
}