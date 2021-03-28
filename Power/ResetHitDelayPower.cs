using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Power
{
    class ResetHitDelayPower : IPower
    {
        public void UsePower(AbstractCharacter self, Board board)
        {
            self.Modifiers.RemoveAll(m => m.ModifierType == Modifier.EnumModifierType.HIT_DELAY);
        }
    }
}
