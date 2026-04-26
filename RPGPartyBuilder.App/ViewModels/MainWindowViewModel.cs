using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RPGPartyBuilder.App.Models;
using RPGPartyBuilder.App.Services;
using System.Linq;
using Avalonia.Markup.Xaml.MarkupExtensions;

namespace RPGPartyBuilder.App.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly PartyManager _partyManager;
    private readonly CharacterTemplateService _characterTemplateService;

    public Party CurrentParty { get; set; }
    
    public int TotalHp => PartyMembers.Sum(character => character.HP);
    public int TotalMp => PartyMembers.Sum(character => character.MP);
    public double AvgPartyLevel => _partyManager.GetPartyAvgLevel(CurrentParty);

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
    
    public void AddCharacter()
    {
        try
        {
            Character newCharacter =
                _characterTemplateService.CreateCharacterFromTemplate(SelectedClassName, CharacterNameInput);

            bool wasAdded = _partyManager.AddCharacterToParty(CurrentParty, newCharacter);
            
            if (wasAdded)
            {
                PartyMembers.Add(newCharacter);
                StatusMessage = $"{newCharacter.Name} added to the party.";
                CharacterNameInput = string.Empty;
                
                OnPropertyChanged(nameof(TotalHp));
                OnPropertyChanged(nameof(TotalMp));
                OnPropertyChanged(nameof(AvgPartyLevel));
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
            
            OnPropertyChanged(nameof(TotalHp));
            OnPropertyChanged(nameof(TotalMp));
            OnPropertyChanged(nameof(AvgPartyLevel));
        }
        else
        {
            StatusMessage = "Could not remove character.";
        }
    }

    public void LevelUpSelectedCharacter()
    {
        if (SelectedCharacter == null)
        {
            StatusMessage = "No character selected.";
            return;
        }
        
        Character characterToLevelUp = SelectedCharacter;
        int index = PartyMembers.IndexOf(characterToLevelUp);

        if (index >= 0)
        {
            PartyMembers.RemoveAt(index);
            PartyMembers.Insert(index, characterToLevelUp);
            SelectedCharacter = characterToLevelUp;
        }

        _partyManager.LevelUpCharacter(SelectedCharacter);
        
        StatusMessage = $"{SelectedCharacter.Name} leveled up!";
        
        // Refreshes stats.
        OnPropertyChanged(nameof(TotalHp));
        OnPropertyChanged(nameof(TotalMp));
        OnPropertyChanged(nameof(AvgPartyLevel));
        OnPropertyChanged(nameof(PartyMembers));
    }

    public void SaveParty()
    {
        try
        {
            _partyManager.SaveParty(CurrentParty, "party.json");
            StatusMessage = "Party saved successfully.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Save failed:  {ex.Message}";
        }
    }

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
            
            OnPropertyChanged(nameof(TotalHp));
            OnPropertyChanged(nameof(TotalMp));
            OnPropertyChanged(nameof(AvgPartyLevel));
            
            StatusMessage = "Party loaded successfully.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Load failed:  {ex.Message}";
        }
    }
}