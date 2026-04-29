using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RPGPartyBuilder.App.Models;
using RPGPartyBuilder.App.Services;
using System.Linq;

namespace RPGPartyBuilder.App.ViewModels;

// Connects the application UI to the Character and Party services.
public class MainWindowViewModel : ViewModelBase
{
    private readonly PartyManager _partyManager;
    private readonly CharacterTemplateService _characterTemplateService;

    public Party CurrentParty { get; set; }
    
    // Calculated Party stats are displayed in the UI.
    public int TotalHp => PartyMembers.Sum(character => character.HP);
    public int TotalMp => PartyMembers.Sum(character => character.MP);
    public double AvgPartyLevel => _partyManager.GetPartyAvgLevel(CurrentParty);

    // Used because ObservableCollection updates the UI when Characters are added or removed.
    public ObservableCollection<Character> PartyMembers { get; set; }

    public List<string> AvailableClasses { get; set; }

    private string _characterNameInput = string.Empty;
    public string CharacterNameInput
    {
        get => _characterNameInput;
        set
        {
            _characterNameInput = value;
            OnPropertyChanged();
        }
    }

    private string _selectedClassName = string.Empty;

    public string SelectedClassName
    {
        get => _selectedClassName;
        set
        {
            _selectedClassName = value;
            OnPropertyChanged();
        }
    }

    private string _statusMessage = string.Empty;

    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            _statusMessage = value;
            OnPropertyChanged();
        }
    }
    
    private Character? _selectedCharacter;
    public Character? SelectedCharacter
    {
        get => _selectedCharacter;
        set
        {
            _selectedCharacter = value;
            OnPropertyChanged();
        }
    }

    // Sets up services, creates the Party, and loads Character Class options.
    public MainWindowViewModel()
    {
        IPartyFileService fileService = new JsonPartyFileService();
        _partyManager = new PartyManager(fileService);
        _characterTemplateService = new CharacterTemplateService();

        CurrentParty = new Party("My Party");
        PartyMembers = new ObservableCollection<Character>();
        AvailableClasses = _characterTemplateService.GetAvailableClasses();

        if (AvailableClasses.Count > 0)
        {
            SelectedClassName = AvailableClasses[0];
        }
    }
    
    // Creates a Character from the Template Service and adds them to the Party.
    public void AddCharacter()
    {
        try
        {
            Character newCharacter = _characterTemplateService.CreateCharacterFromTemplate(SelectedClassName, CharacterNameInput);

            bool wasAdded = _partyManager.AddCharacterToParty(CurrentParty, newCharacter);
            
            if (wasAdded)
            {
                PartyMembers.Add(newCharacter);
                StatusMessage = $"{newCharacter.Name} added to the party.";
                CharacterNameInput = string.Empty;
                
                RefreshPartyStats();
            }
            else
            {
                StatusMessage = $"{newCharacter.Name} could not be added to the party.";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = ex.Message;
        }
    }
    
    // Removes the currently selected Character from the Party.
    public void RemoveCharacter()
    {
        if (SelectedCharacter == null)
        {
            StatusMessage = "No character selected.";
            return;
        }
        
        Character characterToRemove = SelectedCharacter;
        string characterName = characterToRemove.Name;
        
        bool removed = _partyManager.RemoveCharacterFromParty(CurrentParty, characterName);

        if (removed)
        {
            PartyMembers.Remove(SelectedCharacter);
            StatusMessage = $"{characterName} removed from the party.";
            SelectedCharacter = null;
            
            RefreshPartyStats();
        }
        else
        {
            StatusMessage = "Could not remove character.";
        }
    }

    // Levels up the selected Character and refreshes the UI list display.
    public void LevelUpSelectedCharacter()
    {
        if (SelectedCharacter == null)
        {
            StatusMessage = "No character selected.";
            return;
        }
        
        Character characterToLevelUp = SelectedCharacter;
        int index = PartyMembers.IndexOf(characterToLevelUp);
        
        bool leveledUp = _partyManager.LevelUpCharacter(characterToLevelUp);

        if (!leveledUp)
        {
            StatusMessage = $"{characterToLevelUp.Name} is already max level (99)!";
            return;
        }

        // Remove and reinserts so the ListBox updates the changed Character stats.
        if (index >= 0)
        {
            PartyMembers.RemoveAt(index);
            PartyMembers.Insert(index, characterToLevelUp);
            SelectedCharacter = characterToLevelUp;
        }
        
        StatusMessage = $"{SelectedCharacter.Name} leveled up!";
        
        RefreshPartyStats();
    }

    // Sorts Characters by level and rebuilds the UI collection.
    public void SortPartyByLevel()
    {
        _partyManager.SortPartyByLevel(CurrentParty);
        
        PartyMembers.Clear();

        foreach (Character character in CurrentParty.Members)
        {
            PartyMembers.Add(character);
        }
        
        StatusMessage = ("Party sorted by level.");
    }

    // Saves the current Party to a JSON file.
    public void SaveParty()
    {
        try
        {
            _partyManager.SaveParty(CurrentParty, "party.json");
            StatusMessage = "Party saved to party.json.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Save failed: {ex.Message}";
        }
    }

    // Loads the saved Party from a JSON file and refreshes the UI list.
    public void LoadParty()
    {
        try
        {
            CurrentParty = _partyManager.LoadParty("party.json");
            OnPropertyChanged(nameof(CurrentParty));

            PartyMembers.Clear();

            foreach (Character character in CurrentParty.Members)
            {
                PartyMembers.Add(character);
            }
            
            RefreshPartyStats();
            
            StatusMessage = "Party loaded from party.json.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Load failed: {ex.Message}";
        }
    }
    
    // Notifies the UI when calculated Party stats are changed.
    private void RefreshPartyStats()
    {
        OnPropertyChanged(nameof(TotalHp));
        OnPropertyChanged(nameof(TotalMp));
        OnPropertyChanged(nameof(AvgPartyLevel));
    }
}