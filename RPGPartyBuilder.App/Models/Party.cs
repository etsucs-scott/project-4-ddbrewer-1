using System;
using System.Collections.Generic;

namespace RPGPartyBuilder.App.Models;

// Represents a group of Characters, with a size limit of 4 per Party.
public class Party
{
    public string PartyName { get; set; }
    
    public List<Character> Members { get; set; }
    
    public int MaxSize { get; set; } = 4;

    // Default constructor needed for JSON initialization/serialization.
    public Party()
    {
        PartyName = string.Empty;
        Members = new List<Character>();
    }

    // Creates a new Party with a given Party Name.
    public Party(string partyName)
    {
        PartyName = partyName;
        Members = new List<Character>();
    }

    // Returns a Party summary used for UI display.
    public override string ToString()
    {
        return $"{PartyName} - {Members.Count}/{MaxSize} members.";
    }
}