using System;
using RPGPartyBuilder.App.Models;
using RPGPartyBuilder.App.Services;
using Xunit;

namespace RPGPartyBuilder.Tests;

public class RPGPartyManagerTests
{
    private PartyManager CreatePartyManager()
    {
        IPartyFileService fileService = new JsonPartyFileService();
        return new PartyManager(fileService);
    }

    [Fact]
    public void AddCharacterToParty_AddsCharacter_WhenPartyHasSpace()
    {
        PartyManager partyManager = CreatePartyManager();
        Party party = new Party("Test Party");
        Character character = new Character("Dakota", "Warrior", "Tank", 1, 100, 30, 50, 40);
        
        bool result = partyManager.AddCharacterToParty(party, character);
        
        Assert.True(result);
        Assert.Single(party.Members);
    }
    
    [Fact]
    public void AddCharacterToParty_ReturnsFalse_WhenPartyIsFull()
    {
        PartyManager partyManager = CreatePartyManager();
        Party party = new Party("Test Party");

        partyManager.AddCharacterToParty(party, new Character("Character1", "Warrior", "Tank", 1, 100, 30, 50, 40));
        partyManager.AddCharacterToParty(party, new Character("Character2", "Mage", "DPS", 1, 50, 100, 50, 15));
        partyManager.AddCharacterToParty(party, new Character("Character3", "Cleric", "Support", 1, 50, 100, 10, 30));
        partyManager.AddCharacterToParty(party, new Character("Character4", "Rogue", "DPS", 1, 70, 35, 40, 25));
        
        bool result = partyManager.AddCharacterToParty(party, new Character("Character5", "Warrior", "Tank", 1, 100, 30, 50, 40));
        
        Assert.False(result);
        Assert.Equal(4, party.Members.Count);
    }
    
    [Fact]
    public void AddCharacterToParty_ReturnsFalse_WhenNameIsDuplicated()
    {
        PartyManager partyManager = CreatePartyManager();
        Party party = new Party("Test Party");
        
        partyManager.AddCharacterToParty(party, new Character("Character1", "Warrior", "Tank", 1, 100, 30, 50, 40));
        
        bool result = partyManager.AddCharacterToParty(party, new Character("Character1", "Warrior", "Tank", 1, 100, 30, 50, 40));
        
        Assert.False(result);
        Assert.Single(party.Members);
    }
    
    [Fact]
    public void RemoveCharacterFromParty_RemovesCharacter_WhenCharacterExists()
    {
        PartyManager partyManager = CreatePartyManager();
        Party party = new Party("Test Party");
        Character character = new Character("Character1", "Warrior", "Tank", 1, 100, 30, 50, 40);
        
        partyManager.AddCharacterToParty(party, character);
        
        bool result = partyManager.RemoveCharacterFromParty(party, "Character1");
        
        Assert.True(result);
        Assert.Empty(party.Members);
    }
    
    [Fact]
    public void RemoveCharacterFromParty_ReturnsFalse_WhenCharacterDoesNotExist()
    {
        PartyManager partyManager = CreatePartyManager();
        Party party = new Party("Test Party");
        
        bool result = partyManager.RemoveCharacterFromParty(party, "Character1");
        
        Assert.False(result);
    }
    
    [Fact]
    public void GetPartyAverageLevel_ReturnsZero_WhenPartyIsEmpty()
    {
        PartyManager partyManager = CreatePartyManager();
        Party party = new Party("Test Party");
        
        double result = partyManager.GetPartyAvgLevel(party);
        
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetPartyAverageLevel_ReturnsCorrectAverageLevel()
    {
        PartyManager partyManager = CreatePartyManager();
        Party party = new Party("Test Party");
        
        partyManager.AddCharacterToParty(party, new Character("Character1", "Warrior", "Tank", 20, 100, 30, 50, 40));
        partyManager.AddCharacterToParty(party, new Character("Character2", "Mage", "DPS", 10, 50, 100, 50, 15));
        
        double result =  partyManager.GetPartyAvgLevel(party);
        
        Assert.Equal(15, result);
    }
    
    [Fact]
    public void LevelUpCharacter_IncreasesLevelAndStats()
    {
        PartyManager partyManager = CreatePartyManager();
        Character character = new Character("Character1", "Warrior", "Tank", 1, 100, 30, 50, 40);
        
        bool result = partyManager.LevelUpCharacter(character);
        
        Assert.True(result);
        Assert.Equal(2, character.Level);
        Assert.Equal(110, character.HP);
        Assert.Equal(40, character.MP);
        Assert.Equal(55, character.Attack);
        Assert.Equal(43, character.Defense);
    }
    
    [Fact]
    public void LevelUpCharacter_ReturnsFalse_WhenCharacterLevelIs99()
    {
        PartyManager partyManager = CreatePartyManager();
        Character character = new Character("Character1", "Warrior", "Tank", 99, 100, 30, 50, 40);
        
        bool result = partyManager.LevelUpCharacter(character);
        
        Assert.False(result);
        Assert.Equal(99, character.Level);
    }

    [Fact]
    public void CharacterTemplateService_CreatesCharacterFromTemplate()
    {
        CharacterTemplateService characterTemplateService = new CharacterTemplateService();
        
        Character character = characterTemplateService.CreateCharacterFromTemplate("Warrior", "Character1");
        
        Assert.Equal("Character1", character.Name);
        Assert.Equal("Warrior", character.ClassName);
        Assert.Equal("Tank", character.Role);
    }
}
