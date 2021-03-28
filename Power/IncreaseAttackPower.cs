using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Power
{
    class IncreaseAttackPower : IPower
    {
        public void UsePower(AbstractCharacter self, Board board)
        {
            self.CurrentStats.Attack = (int)(self.CurrentStats.Attack * 1.5f);
            self.MyLog($"{self.Name} augmente son attaque à {self.CurrentStats.Attack}");
        }
    }
}
