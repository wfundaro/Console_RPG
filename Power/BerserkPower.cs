using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Power
{
    class BerserkPower : IPower
    {
        public void UsePower(AbstractCharacter self, Board board)
        {
            int PvPerdu = self.BaseStats.MaximumLife - self.CurrentStats.CurrentLife;
            self.CurrentStats.Attack = self.BaseStats.Attack + (PvPerdu/2);
            self.CurrentStats.Damages = self.BaseStats.Damages + (PvPerdu/2);
            int multiAttackSpeed = (int)Math.Floor(( 1.0- (self.CurrentStats.CurrentLife / (float)self.BaseStats.MaximumLife)) * 10.0f);
            self.CurrentStats.AttackSpeed = self.BaseStats.AttackSpeed + (multiAttackSpeed * 0.3f);
            self.MyLog($"{self.Name} attack : {self.CurrentStats.Attack}, damage : {self.CurrentStats.Damages}, attackSpeed : {self.CurrentStats.AttackSpeed}");
        }
    }
}
