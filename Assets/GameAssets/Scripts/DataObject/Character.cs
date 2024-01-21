public class Character
{
    public string Name { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Intelligence { get; set; }

    Character() { }
    Character(string name, int strength, int dexterity, int intelligence)
    {
        Name = name;
        Strength = strength;
        Dexterity = dexterity;
        Intelligence = intelligence;
    }

}
