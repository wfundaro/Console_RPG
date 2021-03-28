using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Power
{
    interface IPower
    {
        public void UsePower(AbstractCharacter self, Board board);
    }
}
