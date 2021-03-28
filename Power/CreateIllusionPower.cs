using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;
using DevoirMaison2021.Character.CharacterDefinition.SpecialCharacters;

namespace DevoirMaison2021.Power
{
    class CreateIllusionPower : IPower
    {
        public void UsePower(AbstractCharacter self, Board board)
        {
            AbstractNoPersistCharacter illusion = new Illusion
            {
                Parent = self
            };
            board.AddCharacter(illusion);
            Illusioniste il = (Illusioniste)self;
            self.MyLog($"{self.Name} ajoute une illusion, il compte désormais {il.CountIllusion()} illusion(s)");
        }
    }
}
