using DevoirMaison2021.Power;
using System;

namespace DevoirMaison2021.Character
{
    class Robot : AbstractCharacter
    {
        public Robot()
        : base("Robot", 25, 100, 1.2f, 50, 275, 275, 0.5f, new IncreaseAttackPower()) { InitOtherStats(); }
        public Robot(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) { InitOtherStats(); }
        private void InitOtherStats()
        {
            AttackRandomRange = (50, 50);
            DefenseRandomRange = (50, 50);
            color = ConsoleColor.Gray;
        }
        public override void InflictedPoison(AbstractCharacter attacker, int totalDamage)
        {
            if (attacker.PercentPoisonDamage > 0)
            {
                MyLog("Les Robots sont insensible au poison Ah! Ah! Ah!");
            }
        }
    }
}
