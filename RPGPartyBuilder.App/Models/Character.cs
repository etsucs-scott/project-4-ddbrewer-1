using System;

namespace RPGPartyBuilder.App.Models;

public class Character
{
    public string Name { get; set; }
    public string ClassName { get; set; }
    public string Role { get; set; }
    
    public int Level { get; set; }
    public int HP { get; set; }
    public int MP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    public Character()
    {
        Name = string.Empty;
        ClassName = string.Empty;
        Role = string.Empty;
    }

    public Character(string name, string className, string role, int level, int hp, int mp, int attack, int defense)
    {
        Name = name;
        ClassName = className;
        Role = role;
        Level = level;
        HP = hp;
        MP = mp;
        Attack = attack;
        Defense = defense;
    }

    public override string ToString()
    {
        return $"{Name} ({ClassName}) - Lv {Level}";
    }
}