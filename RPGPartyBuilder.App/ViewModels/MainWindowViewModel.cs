using System;
using System.Collections.ObjectModel;
using RPGPartyBuilder.App.Models;
using RPGPartyBuilder.App.Services;

namespace RPGPartyBuilder.App.ViewModels;

public class MainWindowViewModel
{
    private readonly PartyManager _partyManager;
    private readonly CharacterTemplateService _characterTemplateService;

    public Party CurrentParty { get; set; }

    public ObservableCollection<Character> PartyMembers { get; set; }

    public List<string> AvailableClasses { get; set; }

    public string CharacterNameInput { get; set; } = string.Empty;

    public string SelectedClassName { get; set; } = string.Empty;

    public string StatusMessage { get; set; } = string.Empty;

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
}