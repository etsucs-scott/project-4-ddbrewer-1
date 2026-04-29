using System;

namespace RPGPartyBuilder.App.Models;

// Represents a single RPG Character with stats and role information.
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

    // Default constructor needed for JSON deserialization.
    public Character()
    {
        Name = string.Empty;
        ClassName = string.Empty;
        Role = string.Empty;
    }

    // Creates a fully initialized Character.
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

    // Used by the UI to display Character information in the list.
    public override string ToString()
    {
        return $"{Name} ({ClassName}) - Lv {Level} -  HP {HP} - MP {MP} - Attack {Attack}  - Defense {Defense} ";
    }
}