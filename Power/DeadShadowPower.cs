using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;

namespace DevoirMaison2021.Power
{
    class DeadShadowPower : IPower
    {
        public void UsePower(AbstractCharacter self, Board board)
        {
            int nbCharacterIsDead = board.BoardCharacters.FindAll(c => c.IsDead).Count;
            if (nbCharacterIsDead == 0)
            {
                self.IsHidden = true;
                self.MyLog($"{self.Name} se camoufle");
            } else
            {
                self.IsHidden = false;
                self.MyLog($"{self.Name} -> DeadShadowPower n'a aucun effet");
            }
        }
    }
}
