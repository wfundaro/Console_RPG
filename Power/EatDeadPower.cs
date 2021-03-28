using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;
using DevoirMaison2021.Character.CharacterDefinition.Mort_Vivants;
using DevoirMaison2021.Character.CharacterDefinition.Vivants;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Power
{
    class EatDeadPower : IPower
    {
        public void UsePower(AbstractCharacter self, Board board)
        {
            List<BoardCharacter> cs = board.BoardCharacters.FindAll(c => c.IsDead && !c.IsEat && (c.Character is Vivant || c.Character is MortVivant));
            if (cs.Count > 0)
            {
                int index = board.Random.Next(cs.Count);
                BoardCharacter deadCharacterNoEat = cs[index];
                if (deadCharacterNoEat != null)
                {
                    self.CurrentStats.CurrentLife = Math.Min(self.BaseStats.MaximumLife, self.CurrentStats.CurrentLife + deadCharacterNoEat.Character.BaseStats.MaximumLife);
                    self.MyLog($"{self.Name} dévore un cadavre et se soigne de {deadCharacterNoEat.Character.BaseStats.MaximumLife}. Sa vie est maintenant de {self.CurrentStats.CurrentLife}");
                    deadCharacterNoEat.IsEat = true;
                }
            }
            else
            {
                self.MyLog($"{self.Name} ne trouve pas de cadavre à dévorer");
            }
        }
    }
}
