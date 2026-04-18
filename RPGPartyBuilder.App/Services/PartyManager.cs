using System;
using System.Collections.Generic;
using System.Linq;

using RPGPartyBuilder.App.Models;

namespace RPGPartyBuilder.App.Services;

public class PartyManager
{
    public bool AddCharacterToParty(Party party, Character character)
    {
        if (party == null)
        {
            throw new ArgumentNullException(nameof(party));
        }

        if (character == null)
        {
            throw new ArgumentNullException(nameof(character));
        }
        
        if (party.Members.Count >= party.MaxSize)
        {
            return false;
        }

        bool duplicateMemberExists =
            party.Members.Any(c => c.Name.Equals(character.Name, StringComparison.OrdinalIgnoreCase));
        if (duplicateMemberExists)
        {
            return false;
        }
        
        party.Members.Add(character);
        return true;
    }

    public bool RemoveCharacterFromParty(Party party, string characterName)
    {
        if (party == null)
        {
            throw new ArgumentNullException(nameof(party));
        }

        if (string.IsNullOrWhiteSpace(characterName))
        {
            throw new ArgumentException("Character name cannot be empty.", nameof(characterName));
        }

        Character? foundCharacter =
            party.Members.FirstOrDefault(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));

        if (foundCharacter == null)
        {
            return false;
        }
        
        party.Members.Remove(foundCharacter);
        return true;
    }

    public int GetPartyHP(Party party)
    {
        if (party == null)
        {
            throw new ArgumentNullException(nameof(party));
        }
        
        return party.Members.Sum(c => c.HP);
    }

    public int GetPartyMP(Party party)
    {
        if (party == null)
        {
            throw new ArgumentNullException(nameof(party));
        }
        
        return party.Members.Sum(c => c.MP);
    }

    public double GetPartyAvgLevel(Party party)
    {
        if (party == null)
        {
            throw new ArgumentNullException(nameof(party));
        }

        if (party.Members.Count == 0)
        {
            return 0;
        }
        
        return party.Members.Average(c => c.Level);
    }
}