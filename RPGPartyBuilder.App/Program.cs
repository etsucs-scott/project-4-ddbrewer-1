using System;
using RPGPartyBuilder.App.Models;
using RPGPartyBuilder.App.Services;

IPartyFileService fileService = new JsonPartyFileService();
PartyManager manager = new PartyManager(fileService);

Party party = new Party("Test Party");

Character c1 = new Character("Character1", "Warrior", "Tank", 100, 30, 50, 50, 30);
Character c2 = new Character("Character2", "Mage", "DPS", 100, 30, 50, 50, 30);

manager.AddCharacterToParty(party, c1);
manager.AddCharacterToParty(party, c2);

manager.SaveParty(party, "party.json");

Party loadedParty = fileService.LoadParty("party.json");

Console.WriteLine($"Party: {loadedParty}");

foreach (Character member in loadedParty.Members)
{
    Console.WriteLine(member);
}