using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character.CharacterDefinition.Vivants;
using DevoirMaison2021.Power;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Character
{
    class Magicien : Vivant
    {
        public Magicien()
        : base("Magicien", 75, 125, 1.5f, 100, 125, 125, 0.1f, new ElectricChainPower()) { color = ConsoleColor.Blue; }
        public Magicien(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) { color = ConsoleColor.Blue; }
    }
}
