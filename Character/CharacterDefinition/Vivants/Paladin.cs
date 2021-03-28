using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character.CharacterDefinition.Vivants;
using DevoirMaison2021.Power;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Character
{
    class Paladin : Vivant
    {
        public Paladin()
        : base("Paladin", 60, 145, 1.6f, 40, 250, 250, 0.5f, new ResetHitDelayPower()) {
            InitOtherStats();
        }

        public Paladin(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) {
            InitOtherStats();
        }
        private void InitOtherStats()
        {
            TypeDeDegat = DegatType.SACRE;
            color = ConsoleColor.Yellow;
        }
    }
}
