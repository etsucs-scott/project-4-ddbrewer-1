using System;
using RPGPartyBuilder.App.Models;

namespace RPGPartyBuilder.App.Services;

// Defines file operations for saving and loading a Party.
// Allows for different implementations to be used (JSON, XML).
public interface IPartyFileService
{
    void SaveParty(Party party, string path);
    
    Party LoadParty(string path);
}