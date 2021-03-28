using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;
using System;
using System.Linq;

namespace DevoirMaison2021.Power
{
    class ChangeLifePower : IPower
    {
        public void UsePower(AbstractCharacter self, Board board)
        {
            AbstractCharacter characterWithMaxCurrentLife = board.BoardCharacters.FindAll(cs => !cs.IsDead).OrderByDescending(c => c.Character.CurrentStats.CurrentLife).First().Character;
            int selfCurrentLife = self.CurrentStats.CurrentLife;
            if(selfCurrentLife < characterWithMaxCurrentLife.CurrentStats.CurrentLife)
            {
                self.CurrentStats.CurrentLife = Math.Min(self.BaseStats.MaximumLife, self.CurrentStats.CurrentLife + characterWithMaxCurrentLife.CurrentStats.CurrentLife);
                self.MyLog($"{self.Name} échange sa vie {self.CurrentStats.CurrentLife} avec {characterWithMaxCurrentLife.Name} qui a {characterWithMaxCurrentLife.CurrentStats.CurrentLife}");
                characterWithMaxCurrentLife.CurrentStats.CurrentLife = selfCurrentLife;
            }
        }
    }
}
 