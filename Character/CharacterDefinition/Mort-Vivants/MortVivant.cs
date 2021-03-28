using DevoirMaison2021.Power;

namespace DevoirMaison2021.Character.CharacterDefinition.Mort_Vivants
{
    abstract class MortVivant : AbstractCharacter
    {
        public MortVivant(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
    : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) { }
        public override void InflictedPoison(AbstractCharacter attacker, int totalDamage)
        {
            if(attacker.PercentPoisonDamage > 0)
            {
                MyLog("Les mort-Vivants sont insensible au poison");
            }
        }
        public override void InflictedSpecial(AbstractCharacter attacker, int normalDamage) {
            if (attacker.TypeDeDegat == DegatType.SACRE)
            {
                CurrentStats.CurrentLife -= normalDamage;
                MyLog($"{Name} subit {normalDamage} de dégats sacrés sa vie est maintenant de {CurrentStats.CurrentLife}");
            }
        }
    }
}
