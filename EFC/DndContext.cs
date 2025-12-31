using EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFC;

public class DndContext : DbContext
{
    public DbSet<Item> Items => Set<Item>();
    public DbSet<PlayerCharacter> PlayerCharacters => Set<PlayerCharacter>();

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        string projectRoot = Directory.GetCurrentDirectory();
        string dbPath = Path.Combine(projectRoot, "dnd.db");
        
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}