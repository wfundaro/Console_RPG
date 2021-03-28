using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;

namespace DevoirMaison2021.Power
{
    class AttackSpeedPower : IPower
    {
        public void UsePower(AbstractCharacter self, Board board)
        {
            Modifier modAttackSpeed = new Modifier(Modifier.EnumModifierType.ATTACK_SPEED, 0.5f, 5000);
            self.Modifiers.Add(modAttackSpeed);
        }
    }
}
