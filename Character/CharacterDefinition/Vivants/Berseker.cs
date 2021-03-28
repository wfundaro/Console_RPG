using DevoirMaison2021.Character.CharacterDefinition.Vivants;
using DevoirMaison2021.Power;
using System;

namespace DevoirMaison2021.Character
{
    class Berseker : Vivant
    {
        public Berseker()
        : base("Berseker", 50, 50, 1.1f, 20, 400, 400, 1.0f, new BerserkPower()) {
            color = ConsoleColor.DarkMagenta;
        }
        public Berseker(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) {
            color = ConsoleColor.DarkMagenta;
        }
        public override void InflictedHitDelay(int normalDamage)
        {
            if (normalDamage > 0 && (CurrentStats.CurrentLife / normalDamage) <= 2)
            {
                Modifier hitDelay = new Modifier(Modifier.EnumModifierType.HIT_DELAY, normalDamage);
                Modifiers.Add(hitDelay);
            }
        }
        public override void UsePower()
        {
            Power.UsePower(this, Board);
        }
    }
}
