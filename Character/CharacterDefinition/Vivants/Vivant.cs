using DevoirMaison2021.Power;

namespace DevoirMaison2021.Character.CharacterDefinition.Vivants
{
    abstract class Vivant : PoisonSensible
    {
        public Vivant(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
: base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) { }
        public override void InflictedPoison(AbstractCharacter attacker, int totalDamage)
        {
            int poisonDamage = (int)(totalDamage * attacker.PercentPoisonDamage);
            if (poisonDamage > 0)
            {
                PoisonRate += poisonDamage;
                MyLog($"{Name} est empoisonné son taux d'empoisonnement est de {PoisonRate}");
            }
        }
    }
}
