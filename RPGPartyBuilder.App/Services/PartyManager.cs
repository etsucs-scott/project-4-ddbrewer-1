using System;
using System.Collections.Generic;
using System.Linq;
using RPGPartyBuilder.App.Models;

namespace RPGPartyBuilder.App.Services;

// Handles all Party-related logic (adding, removing, leveling, stats, sorting).
public class PartyManager
{
    // Adds a Character to the Party if it is not full and no duplicate Character name exists.
    public bool AddCharacterToParty(Party party, Character character)
    {
        if (party == null) throw new ArgumentNullException(nameof(party));

        if (character == null) throw new ArgumentNullException(nameof(character));
        
        // Enforces maximum Party size.
        if (party.Members.Count >= party.MaxSize) return false;

        // Prevents duplicate Character names.
        bool duplicateMemberExists = party.Members.Any(c => c.Name.Equals(character.Name, StringComparison.OrdinalIgnoreCase));
        
        if (duplicateMemberExists) return false;
        
        party.Members.Add(character);
        
        return true;
    }

    // Removes a Character by name if found.
    public bool RemoveCharacterFromParty(Party party, string characterName)
    {
        if (party == null) throw new ArgumentNullException(nameof(party));

        if (string.IsNullOrWhiteSpace(characterName)) throw new ArgumentException("Character name cannot be empty.", nameof(characterName));

        Character? foundCharacter = party.Members.FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

        if (foundCharacter == null) return false;
        
        party.Members.Remove(foundCharacter);
        
        return true;
    }

    // Increases Character level and stats, up to a maximum cap of 99.
    public bool LevelUpCharacter(Character character)
    {
        if (character == null) throw new ArgumentNullException(nameof(character));

        if (character.Level >= 99) return false;
        
        // Simple stat scaling, to keep it readable.
        character.Level+=1;
        character.HP += 10;
        character.MP += 10;
        character.Attack += 5;
        character.Defense += 3;
        
        return true;
    }

    // Returns total HP of all Party members.
    public int GetPartyHP(Party party)
    {
        if (party == null) throw new ArgumentNullException(nameof(party));
        
        return party.Members.Sum(c => c.HP);
    }

    // Returns total MP of all Party members.
    public int GetPartyMP(Party party)
    {
        if (party == null) throw new ArgumentNullException(nameof(party));
        
        return party.Members.Sum(c => c.MP);
    }

    // Returns the average level of a Party (returns 0 if Party is empty).
    public double GetPartyAvgLevel(Party party)
    {
        if (party == null) throw new ArgumentNullException(nameof(party));

        if (party.Members.Count == 0) return 0;
        
        return party.Members.Average(c => c.Level);
    }

    // Sorts Party members in descending order by level.
    public void SortPartyByLevel(Party party)
    {
        if (party == null) throw new ArgumentNullException(nameof(party));
        
        party.Members = party.Members
            .OrderByDescending(c => c.Level)
            .ToList();
    }

    // File service dependencies.
    private readonly IPartyFileService _partyFileService;

    public PartyManager(IPartyFileService partyFileService)
    {
        _partyFileService = partyFileService;
    }

    public void SaveParty(Party party, string path)
    {
        _partyFileService.SaveParty(party, path);
    }
    
    public Party LoadParty(string path)
    {
        return _partyFileService.LoadParty(path);
    }
}