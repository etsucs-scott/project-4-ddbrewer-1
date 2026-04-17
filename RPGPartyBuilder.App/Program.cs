using System;
using RPGPartyBuilder.App.Models;

Character c1 =  new Character("Dakota", "Warrior", "Tank", 1, 100, 50, 20, 10);

Party party = new Party("My First Party");
party.Members.Add(c1);

Console.WriteLine(party.PartyName);
Console.WriteLine(party.Members[0]);