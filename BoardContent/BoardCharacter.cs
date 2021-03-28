using DevoirMaison2021.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.BoardContent
{
    class BoardCharacter
    {
        public AbstractCharacter Character { get; set; }
        public bool IsDead { get; set; }
        public bool IsEat { get; set; }
        public BoardCharacter(AbstractCharacter character)
        {
            Character = character;
            IsDead = false;
            IsEat = false;
        }
    }
}
