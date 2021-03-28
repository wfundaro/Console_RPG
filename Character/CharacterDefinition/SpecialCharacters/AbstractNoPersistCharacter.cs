using DevoirMaison2021.Power;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Character.CharacterDefinition.SpecialCharacters
{
    abstract class AbstractNoPersistCharacter : AbstractCharacter
    {
        public AbstractCharacter Parent { get; set; }
        public AbstractNoPersistCharacter(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
    : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) { }
    }
}
