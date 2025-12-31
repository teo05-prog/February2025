namespace EFC.Entities;

public class PlayerCharacter
{
    public int PlayerCharacterId { get; set; }
    public string Class { get; set; }
    public string Race { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();

    public PlayerCharacter()
    {
        Class = string.Empty;
        Race = string.Empty;
        Name = string.Empty;
    }

    public PlayerCharacter(string name, string characterClass, string race, int level)
    {
        Name = name;
        Class = characterClass;
        Race = race;
        Level = level;
    }
}