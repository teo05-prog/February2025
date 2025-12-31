using EFC.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFC;

public class DataAccess
{
    private DndContext context;
    
    public DataAccess()
    {
        context = new DndContext();
        context.Database.EnsureCreated();
    }
    
    public async Task AddCharacterAsync(PlayerCharacter character)
    {
        context = new DndContext();
        context.PlayerCharacters.Add(character);
        await context.SaveChangesAsync();
    }
    
    public async Task AddItemToCharacterAsync(int characterId, Item item)
    {
        context = new DndContext();
        var character = await context.PlayerCharacters.FindAsync(characterId);
        if (character != null)
        {
            item.PlayerCharacterId = characterId;
            context.Items.Add(item);
            await context.SaveChangesAsync();
        }
    }
    
    public async Task<List<Item>> GetItemsOfCharacterAsync(int characterId, string? type = null)
    {
        context = new DndContext();
        var query = context.Items.Where(i => i.PlayerCharacterId == characterId);
        
        if (!string.IsNullOrEmpty(type))
        {
            query = query.Where(i => i.Type == type);
        }
        
        return await query.ToListAsync();
    }

    public async Task<List<PlayerCharacter>> GetPlayerCharactersByRankAsync()
    {
        context = new DndContext();
        
        // Get all characters with their items
        var characters = await context.PlayerCharacters
            .Include(pc => pc.Items)
            .ToListAsync();
        
        // Sort by level (descending), then by value-to-weight ratio
        var ranked = characters
            .OrderByDescending(c => c.Level)
            .ThenByDescending(c => 
            {
                var nonJunkItems = c.Items.Where(i => i.Type != "Junk").ToList();
                if (nonJunkItems.Count == 0) return 0;
                
                var totalValue = nonJunkItems.Sum(i => i.Value);
                var totalWeight = nonJunkItems.Sum(i => i.Weight);
                
                return totalWeight > 0 ? (double)totalValue / totalWeight : 0;
            })
            .ToList();
        
        return ranked;
    }
}