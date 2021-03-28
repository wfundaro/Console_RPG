using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character.CharacterDefinition.Mort_Vivants;
using DevoirMaison2021.Character.CharacterDefinition.Vivants;
using DevoirMaison2021.Power;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Character
{
    class Pretre : Vivant
    {
        public Pretre()
        : base("Pretre", 100, 125, 1.5f, 90, 150, 150, 1.0f, new HolyHealPower())
        {
            InitOtherStats();
        }
        public Pretre(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) {
            InitOtherStats();
        }
        private void InitOtherStats()
        {
            TypeDeDegat = DegatType.SACRE;
            color = ConsoleColor.White;
        }
        public override void LaunchAttack()
        {
            List<BoardCharacter> listUndead = Board.BoardCharacters.FindAll(bc => !bc.IsDead && bc.Character is MortVivant);
            BoardCharacter target;
            if(listUndead.Count > 0)
            {
                target = listUndead[Board.Random.Next(listUndead.Count - 1)];
            } else
            {
                target = Board.GetRandomCharacterNoDead(this);
            }

            if (target != null)
            {
                int attackJet = CurrentStats.Attack + Board.Random.Next(AttackRandomRange.Item1, AttackRandomRange.Item2);
                MyLog($"{Name} attaque {target.Character.Name}");
                target.Character.Launchdefense(this, attackJet);
            }
            else
            {
                MyLog($"{Name} ne trouve personne à attaquer");
            }
        }
    }
}
