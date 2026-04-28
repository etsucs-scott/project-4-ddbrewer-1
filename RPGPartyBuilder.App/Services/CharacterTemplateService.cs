using System;
using System.Collections.Generic;
using System.Linq;

using RPGPartyBuilder.App.Models;

namespace RPGPartyBuilder.App.Services;

// Provides predefined Character templates and creates Characters from them.
public class CharacterTemplateService
{
    // Stores base templates for each class (with case-insensitive keys).
    private readonly Dictionary<string, Character> _templates;
    
    public CharacterTemplateService()
    {
        _templates = new Dictionary<string, Character>(StringComparer.OrdinalIgnoreCase)
        {
            {
                "Warrior",
                new Character("", "Warrior", "Tank", 1, 100, 30, 50, 40)
            },
            {
                "Mage",
                new Character("", "Mage", "DPS", 1, 50, 100, 50, 15)
            },
            {
                "Cleric",
                new Character("", "Cleric", "Support", 1, 50, 100, 10, 30)
            },
            {
                "Rogue",
                new Character("", "Rogue", "DPS", 1, 70, 35, 40, 25)
            }
        };
    }
    
    // Returns the list of available classes for UI selection (drop-down menu).
    public List<string> GetAvailableClasses()
    {
        return _templates.Keys.ToList();
    }

    // Creates a Character from the Template and assigns them the user's provided name.
    public Character CreateCharacterFromTemplate(string className, string characterName)
    {
        // Input validation to prevent bad Character creation.
        if (string.IsNullOrWhiteSpace(className)) throw new ArgumentException("Class name cannot be empty.", nameof(className));

        if (string.IsNullOrWhiteSpace(characterName)) throw new ArgumentException("Character name cannot be empty.", nameof(characterName));

        if (!_templates.ContainsKey(className)) throw new ArgumentException("That class does not exist.", nameof(className));
        
        Character template = _templates[className];

        // Copies the Template stats into a new Character instance.
        return new Character(characterName, template.ClassName, template.Role, template.Level, template.HP, template.MP, template.Attack, template.Defense);
    }
}