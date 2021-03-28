using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Power
{
    class WindShearPower : IPower
    {
        private int Life { get; set; }
        public void UsePower(AbstractCharacter self, Board board)
        {
            AbstractCharacter randomTarget = board.GetRandomCharacterNoDead(self).Character;
            int nbTotalDamages = Math.Max(0, self.NbDamageTaken - Life);
            Life = self.NbDamageTaken;
            if (nbTotalDamages > 0)
            {
                Modifier modifier = new Modifier(Modifier.EnumModifierType.HIT_DELAY, nbTotalDamages);
                randomTarget.Modifiers.Add(modifier);
                self.MyLog($"{self.Name} pénalise {randomTarget.Name}, hit delay + {nbTotalDamages}");
            }
        }
    }
}
