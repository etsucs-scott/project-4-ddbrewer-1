using System;
using System.IO;
using System.Text.Json;
using RPGPartyBuilder.App.Models;

namespace RPGPartyBuilder.App.Services;

public class JsonPartyFileService : IPartyFileService
{
    public void SaveParty(Party party, string path)
    {
        try
        {
            string json = JsonSerializer.Serialize(party, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error saving party: {ex.Message}");
        }
    }

    public Party LoadParty(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Party file not found: {path}");
            }

            string json = File.ReadAllText(path);

            Party? party = JsonSerializer.Deserialize<Party>(json);

            if (party == null)
            {
                throw new Exception($"Failed to load party data: {path}");
            }

            return party;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error loading party file: {ex.Message}");
        }
    }
}    