using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character.CharacterDefinition.Mort_Vivants;
using DevoirMaison2021.Power;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Character
{
    class Necromancien : MortVivant
    {
        public Necromancien()
        : base("Necromancien", 0, 10, 1.0f, 0, 275, 275, 5.0f, new DeadShadowPower())
        {
            InitOtherStats();
        }
        public Necromancien(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) {
            InitOtherStats();
        }
        private void InitOtherStats()
        {
            AttackRandomRange = (1, 150);
            DefenseRandomRange = (1, 150);
            PercentPoisonDamage = 0.5f;
            PercentNormalDamage = 0.5f;
            CanUseCamouflage = true;
            color = ConsoleColor.DarkCyan;
        }
        public override void OtherCharacterIsDead(AbstractCharacter characterDead)
        {
            CurrentStats.Attack += 5;
            CurrentStats.Defense += 5;
            CurrentStats.Damages += 5;
            CurrentStats.CurrentLife += 50;
            CurrentStats.MaximumLife += 50;
            MyLog($"{Name} profite de la mort de {characterDead.Name} et augmente ses attributs");
            if (!(characterDead is Illusion))
            {
                IsHidden = false;
            }
        }
    }
}
