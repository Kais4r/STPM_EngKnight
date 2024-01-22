public class Character
{
    public string Name { get; set; }
    public int Attack { get; set; }
    public int HP { get; set; }
    public int HitChance { get; set; }
    public int Invasion { get; set; }

    public Character() { }
    public Character(string name, int attack, int hP, int hitChance, int invasion)
    {
        Name = name;
        Attack = attack;
        HP = hP;
        HitChance = hitChance;
        Invasion = invasion;
    }

    // Method
    public void AttackCharacter(Character target)
    {
        target.HP -= this.Attack;
    }
}
