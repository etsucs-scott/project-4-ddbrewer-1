using System;
using RPGPartyBuilder.App.Models;

namespace RPGPartyBuilder.App.Services;

public interface IPartyFileService
{
    void SaveParty(Party party, string path);
    Party LoadParty(string path);
}