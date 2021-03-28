using DevoirMaison2021.Character.CharacterDefinition.Vivants;
using DevoirMaison2021.Power;
using System;

namespace DevoirMaison2021.Character
{
    class Guerrier : Vivant
    {
        public Guerrier(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) {
            color = ConsoleColor.Green;
        }
        public Guerrier()
        : base("Guerrier", 150, 105, 2.2f, 150, 250, 250, 0.2f, new AttackSpeedPower()) {
            color = ConsoleColor.Green;
        }
    }
}
