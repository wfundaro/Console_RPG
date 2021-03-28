using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character.CharacterDefinition.Vivants;
using DevoirMaison2021.Power;
using System;
using System.Collections.Generic;

namespace DevoirMaison2021.Character
{
    class Illusioniste : Vivant
    {
        public Illusioniste()
        : base("Illusioniste", 75, 75, 1.0f, 50, 100, 100, 0.5f, new CreateIllusionPower()) {
            color = ConsoleColor.Cyan;
                }
        public Illusioniste(string name, int attack, int defense, float attackSpeed, int damages, int maximumLife, int currentLife, float powerSpeed, IPower _power)
            : base(name, attack, defense, attackSpeed, damages, maximumLife, currentLife, powerSpeed, _power) {
            color = ConsoleColor.Cyan;
        }
        public override void LaunchAttack()
        {
            List<BoardCharacter> copyListBoardCharacters = new List<BoardCharacter>(Board.BoardCharacters.FindAll(bc => !bc.IsDead));
            // remove Illusion de l'illusioniste
            copyListBoardCharacters.RemoveAll(bc => bc.Character is Illusion illusion && illusion.Parent == this);
            if (copyListBoardCharacters.Count > 0)
            {
                int index = Board.Random.Next(copyListBoardCharacters.Count - 1);
                int attackJet = CurrentStats.Attack + (10 * CountIllusion()) + Board.Random.Next(AttackRandomRange.Item1, AttackRandomRange.Item2);
                copyListBoardCharacters[index].Character.Launchdefense(this, attackJet);
            }
        }
        public override int Launchdefense(AbstractCharacter attacker, int attackJet)
        {
            List<BoardCharacter> illusions = Board.BoardCharacters.FindAll(il => il.Character is Illusion illusion && illusion.Parent == this);
            int cibleIllusion = Board.Random.Next(1 + illusions.Count);
            if(cibleIllusion == 0)  // l'illusioniste est bien attaqué
            {
                return base.Launchdefense(attacker, attackJet);
            } else
            {
                // une illusion est attaquée à la place
                MyLog("L'illusioniste déporte l'attaque sur une illusion");
                return illusions[Board.Random.Next(illusions.Count)].Character.Launchdefense(attacker, attackJet);
            }

        }
        public int CountIllusion()
        {
            int nbIllusion = Board.BoardCharacters.FindAll(il => il.Character is Illusion illusion && illusion.Parent == this).Count;
            return nbIllusion;
        }
    }
}
