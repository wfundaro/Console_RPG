using DevoirMaison2021.Power;
using System;

namespace DevoirMaison2021.Character.CharacterDefinition.Vivants
{
    class PoisonSensible : AbstractCharacter
    {
        public PoisonSensible(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
: base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) { }
        public override void LaunchAction(long deltaTime)
        {
            if (PoisonRate > 0)
            {
                TimePoisonDamage += deltaTime;
                if (TimePoisonDamage >= TIME_TO_APPLY_POISON)
                {
                    CurrentStats.CurrentLife -= PoisonRate;
                    TimePoisonDamage = 0;
                }
            }
            if (CurrentStats.CurrentLife <= 0)
            {
                EventDead(EventArgs.Empty);
            } else
            {
                base.LaunchAction(deltaTime);
            }
        }
    }

}
