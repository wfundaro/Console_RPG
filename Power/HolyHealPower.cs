using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;
using System;

namespace DevoirMaison2021.Power
{
    class HolyHealPower : IPower
    {
        public void UsePower(AbstractCharacter self, Board board)
        {
            int nbPointOfHeal = self.BaseStats.MaximumLife / 10;
            self.MyLog($"{self.Name} se soigne de {nbPointOfHeal} points de vie.");
            self.CurrentStats.CurrentLife = Math.Min(self.BaseStats.MaximumLife , self.CurrentStats.CurrentLife + nbPointOfHeal);
        }
    }
}
