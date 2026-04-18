using System;
using RPGPartyBuilder.App.Models;
using RPGPartyBuilder.App.Services;

CharacterTemplateService characterTemplateService = new CharacterTemplateService();

Console.WriteLine("Available Classes:");

foreach (string className in characterTemplateService.GetAvailableClasses())
{
    Console.WriteLine(className);
}

Character mage = characterTemplateService.CreateCharacterFromTemplate("Mage", "Luna");

Console.WriteLine();
Console.WriteLine($"Name: {mage.Name}");
Console.WriteLine($"Class: {mage.ClassName}");
Console.WriteLine($"Role: {mage.Role}");
Console.WriteLine($"Level: {mage.Level}");
Console.WriteLine($"HP: {mage.HP}");
Console.WriteLine($"MP: {mage.MP}");
Console.WriteLine($"Attack: {mage.Attack}");
Console.WriteLine($"Defense: {mage.Defense}");