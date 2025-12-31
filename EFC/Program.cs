using EFC;
using EFC.Entities;
using Microsoft.EntityFrameworkCore;

var context = new DndContext();

// Create the database if it doesn't exist
context.Database.EnsureCreated();

var dataAccess = new DataAccess();

Console.WriteLine("=== D&D Database ===\n");

// Add Characters
Console.WriteLine("--- Adding Characters ---");

var aragorn = new PlayerCharacter("Aragorn", "Ranger", "Human", 10);
await dataAccess.AddCharacterAsync(aragorn);
Console.WriteLine($"Added: {aragorn.Name} (Level {aragorn.Level} {aragorn.Race} {aragorn.Class})");

var gandalf = new PlayerCharacter("Gandalf", "Wizard", "Human", 15);
await dataAccess.AddCharacterAsync(gandalf);
Console.WriteLine($"Added: {gandalf.Name} (Level {gandalf.Level} {gandalf.Race} {gandalf.Class})");

var legolas = new PlayerCharacter("Legolas", "Archer", "Elf", 12);
await dataAccess.AddCharacterAsync(legolas);
Console.WriteLine($"Added: {legolas.Name} (Level {legolas.Level} {legolas.Race} {legolas.Class})");

Console.WriteLine();

// Get the characters back from the database to get their IDs
var allCharacters = await context.PlayerCharacters.ToListAsync();
var aragornDb = allCharacters.FirstOrDefault(c => c.Name == "Aragorn");
var gandalfDb = allCharacters.FirstOrDefault(c => c.Name == "Gandalf");
var legolasDb = allCharacters.FirstOrDefault(c => c.Name == "Legolas");

// Add Items to Characters
Console.WriteLine("--- Adding Items to Characters ---");

if (aragornDb != null)
{
    var sword = new Item("Longsword", "Weapon", 15, 3.5f);
    await dataAccess.AddItemToCharacterAsync(aragornDb.PlayerCharacterId, sword);
    Console.WriteLine($"Added to {aragornDb.Name}: {sword.Name} ({sword.Type})");

    var shield = new Item("Iron Shield", "Armor", 10, 6f);
    await dataAccess.AddItemToCharacterAsync(aragornDb.PlayerCharacterId, shield);
    Console.WriteLine($"Added to {aragornDb.Name}: {shield.Name} ({shield.Type})");

    var junk = new Item("Broken Rope", "Junk", 1, 0.5f);
    await dataAccess.AddItemToCharacterAsync(aragornDb.PlayerCharacterId, junk);
    Console.WriteLine($"Added to {aragornDb.Name}: {junk.Name} ({junk.Type})");
}

if (gandalfDb != null)
{
    var staff = new Item("Elven Staff", "Weapon", 50, 2f);
    await dataAccess.AddItemToCharacterAsync(gandalfDb.PlayerCharacterId, staff);
    Console.WriteLine($"Added to {gandalfDb.Name}: {staff.Name} ({staff.Type})");

    var potion = new Item("Minor Healing Potion", "Potion", 20, 0.5f);
    await dataAccess.AddItemToCharacterAsync(gandalfDb.PlayerCharacterId, potion);
    Console.WriteLine($"Added to {gandalfDb.Name}: {potion.Name} ({potion.Type})");

    var book = new Item("Ancient Tome", "Potion", 100, 3f);
    await dataAccess.AddItemToCharacterAsync(gandalfDb.PlayerCharacterId, book);
    Console.WriteLine($"Added to {gandalfDb.Name}: {book.Name} ({book.Type})");
}

if (legolasDb != null)
{
    var bow = new Item("Elven Bow", "Weapon", 45, 1.5f);
    await dataAccess.AddItemToCharacterAsync(legolasDb.PlayerCharacterId, bow);
    Console.WriteLine($"Added to {legolasDb.Name}: {bow.Name} ({bow.Type})");

    var arrows = new Item("Quiver of Arrows", "Weapon", 8, 1f);
    await dataAccess.AddItemToCharacterAsync(legolasDb.PlayerCharacterId, arrows);
    Console.WriteLine($"Added to {legolasDb.Name}: {arrows.Name} ({arrows.Type})");

    var trash = new Item("Old Cloth", "Junk", 0, 0.1f);
    await dataAccess.AddItemToCharacterAsync(legolasDb.PlayerCharacterId, trash);
    Console.WriteLine($"Added to {legolasDb.Name}: {trash.Name} ({trash.Type})");
}

Console.WriteLine();

// Get Items for Character (with optional type filter)
Console.WriteLine("--- Getting Items for Characters ---");

if (aragornDb != null)
{
    Console.WriteLine($"\nAll items for {aragornDb.Name}:");
    var allItems = await dataAccess.GetItemsOfCharacterAsync(aragornDb.PlayerCharacterId);
    foreach (var item in allItems)
    {
        Console.WriteLine($"  - {item.Name} ({item.Type}): {item.Value}g, {item.Weight}lb");
    }

    Console.WriteLine($"\nWeapon items for {aragornDb.Name}:");
    var weapons = await dataAccess.GetItemsOfCharacterAsync(aragornDb.PlayerCharacterId, "Weapon");
    foreach (var item in weapons)
    {
        Console.WriteLine($"  - {item.Name} ({item.Type}): {item.Value}g, {item.Weight}lb");
    }
}

Console.WriteLine();

// Get Characters Ranked
Console.WriteLine("--- Characters Ranked by Level and Value-to-Weight Ratio ---");
var rankedCharacters = await dataAccess.GetPlayerCharactersByRankAsync();

foreach (var character in rankedCharacters)
{
    var nonJunkItems = character.Items.Where(i => i.Type != "Junk").ToList();
    double ratio = 0;
    if (nonJunkItems.Count > 0)
    {
        var totalValue = nonJunkItems.Sum(i => i.Value);
        var totalWeight = nonJunkItems.Sum(i => i.Weight);
        ratio = totalWeight > 0 ? (double)totalValue / totalWeight : 0;
    }

    Console.WriteLine($"{character.Name} (Level {character.Level}, {character.Race} {character.Class}) - Value/Weight Ratio: {ratio:F2}");
    Console.WriteLine($"  Items: {character.Items.Count}");
}

