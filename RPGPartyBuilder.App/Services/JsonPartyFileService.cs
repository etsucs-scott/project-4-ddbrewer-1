using System;
using System.IO;
using System.Text.Json;
using RPGPartyBuilder.App.Models;

namespace RPGPartyBuilder.App.Services;

// Implements the IPartyFileService for saving/loading with JSON format.
public class JsonPartyFileService : IPartyFileService
{
    // Serializes the Party object and writes it to the file path.
    public void SaveParty(Party party, string path)
    {
        try
        {
            string json = JsonSerializer.Serialize(party, new JsonSerializerOptions
            {
                WriteIndented = true // Makes the JSON file readable.
            });
            
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error saving party: {ex.Message}");
        }
    }

    // Reads the JSON file and deserializes it into a Party object.
    public Party LoadParty(string path)
    {
        try
        {
            // Ensures file path exists before trying to read.
            if (!File.Exists(path)) throw new FileNotFoundException($"Party file not found: {path}");

            string json = File.ReadAllText(path);

            Party? party = JsonSerializer.Deserialize<Party>(json);

            // Exception to handle failed deserialization.
            if (party == null) throw new Exception($"Failed to load party data: {path}");

            return party;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error loading party file: {ex.Message}");
        }
    }
}    