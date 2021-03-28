using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character.CharacterDefinition.Mort_Vivants;
using DevoirMaison2021.Power;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Character
{
    class Vampire : MortVivant
    {
        public Vampire()
        : base("Vampire", 125, 125, 2.0f, 50, 150, 150, 0.2f, new WindShearPower()) {
            color = ConsoleColor.DarkRed;
        }
        public Vampire(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) {
            color = ConsoleColor.DarkRed;
        }
        public override void SpecialAttack(BoardCharacter target, int damageInflicted) 
        {
            if(damageInflicted > 0)
            {
                MyLog($"{Name} se soigne 50% de {damageInflicted} sa vie {CurrentStats.CurrentLife} augmente de {damageInflicted / 2}");
                CurrentStats.CurrentLife += (damageInflicted / 2);
            }
        }
    }
}
