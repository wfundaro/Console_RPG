using DevoirMaison2021.Character.CharacterDefinition.SpecialCharacters;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Character
{
    class Illusion : AbstractNoPersistCharacter
    {
        public Illusion()
        : base("Illusion", 0, 75, 0f, 1, 1, 1, 0f, null) { }

        public override void LaunchAction(long deltaTime) { 
            // aucune action pour les illusion
        }
    }
}
