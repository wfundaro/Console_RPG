using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;

namespace DevoirMaison2021.Power
{
    class ShadowPower : IPower
    {
        public void UsePower(AbstractCharacter self, Board board)
        {
            int nbCharacterIsAlive = board.BoardCharacters.FindAll(c => !c.IsDead).Count;
            if(nbCharacterIsAlive > 4)
            {
                self.IsHidden = true;
                self.MyLog($"{self.Name} se camoufle");
            } else
            {
                self.IsHidden = false;
                self.MyLog($"{self.Name} -> ShadowPower n'a aucun effet");
            }
        }
    }
}
