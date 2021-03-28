using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character.CharacterDefinition.Vivants;
using DevoirMaison2021.Power;
using System;

namespace DevoirMaison2021.Character
{
    class Assassin : Vivant
    {
        public Assassin()
        : base("Assassin", 150, 100, 1.0f, 100, 185, 185, 0.5f, new ShadowPower())
        {
            InitOtherStats();
        }
        public Assassin(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) {
            InitOtherStats();
        }
        private void InitOtherStats()
        {
            CanUseCamouflage = true;
            PercentPoisonDamage = 0.1f;
            PercentNormalDamage = 1.0f;
            color = ConsoleColor.Red;
        }
        public override void SpecialAttack(BoardCharacter target, int damageInflicted) {
            if (!target.IsDead && damageInflicted > target.Character.CurrentStats.CurrentLife)
            {
                MyLog($"L'assassin inflige un coup critique {damageInflicted} vie cible {target.Character.CurrentStats.CurrentLife}");
                target.Character.CurrentStats.CurrentLife = 0;
                target.Character.CharacterDead();
            }
        }
        public override void OtherCharacterIsDead(AbstractCharacter characterDead)
        {
            int nbCharacterInLive = Board.BoardCharacters.FindAll(bc => !bc.IsDead && !(bc.Character is Illusion)).Count;
            // Si moins de 5 personnages on supprime le camouflage (on ne tient pas compte des illusions)
            if (nbCharacterInLive < 5)
            {
                IsHidden = false;
                MyLog($"{Name} ne peut plus se camoufler");
            }
        }
    }
}
