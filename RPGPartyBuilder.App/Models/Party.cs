using System;
using System.Collections.Generic;

namespace RPGPartyBuilder.App.Models;

public class Party
{
    public string PartyName { get; set; }
    
    public List<Character> Members { get; set; }

    public int MaxSize { get; set; } = 4;

    public Party()
    {
        PartyName = string.Empty;
        Members = new List<Character>();
    }

    public Party(string partyName)
    {
        PartyName = partyName;
        Members = new List<Character>();
    }

    public override string ToString()
    {
        return $"{PartyName} - {Members.Count}/{MaxSize} members.";
    }
}