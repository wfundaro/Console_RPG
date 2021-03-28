using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character.CharacterDefinition.Vivants;
using DevoirMaison2021.Power;
using System;
using System.Collections.Generic;

namespace DevoirMaison2021.Character.CharacterDefinition
{
    class Alchimiste : Vivant
    {
        public Alchimiste()
        : base("Alchimiste", 50, 50, 1.0f, 30, 150, 150, 0.1f, new ChangeLifePower())
        {
            InitOtherStats();
        }
        public Alchimiste(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power)
        {
            InitOtherStats();
        }
        private void InitOtherStats()
        {
            TypeDeDegat = DegatType.SACRE;
            AttackRandomRange = (1, 200);
            DefenseRandomRange = (1, 200);
            PercentPoisonDamage = 0.5f;
            PercentNormalDamage = 1.0f;
            color = ConsoleColor.Magenta;
        }
        public override void LaunchAttack()
        {
            List<BoardCharacter> cs = Board.BoardCharacters.FindAll(c => c.IsDead == false && c.Character != this);
            int attackJet = CurrentStats.Attack + Board.Random.Next(AttackRandomRange.Item1, AttackRandomRange.Item2 + 1);
            foreach(BoardCharacter target in cs)
            {
                if (Board.Random.Next(2) == 1)
                {
                    MyLog($"{Name} attaque de zone {target.Character.Name}");
                    int damage = target.Character.Launchdefense(this, attackJet);
                }
            }
        }
    }
}
