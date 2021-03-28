using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character.CharacterDefinition.Mort_Vivants;
using DevoirMaison2021.Power;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Character
{
    class Zombie : MortVivant
    {
        public Zombie()
        : base("Zombie", 150, 0, 1.0f, 20, 1500, 1500, 0.1f, new EatDeadPower()) { InitOtherStats(); }
        public Zombie(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) { InitOtherStats(); }
        private void InitOtherStats()
        {
            DefenseRandomRange = (0, 0);
            color = ConsoleColor.DarkYellow;
        }
        public override void InflictedHitDelay(int normalDamage){ /* no hit delay */ }
    }
}
