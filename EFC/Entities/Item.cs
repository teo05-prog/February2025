namespace EFC.Entities;

public class Item
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Value { get; set; }
    public float Weight { get; set; }
    public int PlayerCharacterId { get; set; }
    
    public Item()
    {
        Name = string.Empty;
        Type = string.Empty;
    }
    
    public Item(string name, string type, int value, float weight)
    {
        Name = name;
        Type = type;
        Value = value;
        Weight = weight;
    }
}