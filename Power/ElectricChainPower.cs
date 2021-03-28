using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Power
{
    class ElectricChainPower : IPower
    {
        public void UsePower(AbstractCharacter self, Board board)
        {
            int damage = 0;
            int numCharacter = 0;
            List<BoardCharacter> listOfCharacter = new List<BoardCharacter>(board.BoardCharacters.FindAll(bc => !bc.IsDead && bc.Character != self));
            do
            {
                damage = 0;
                if (listOfCharacter.Count > 0) {
                    int indexCharacter = board.Random.Next(listOfCharacter.Count);
                    int attackJet = (self.CurrentStats.Attack * 5) + board.Random.Next(self.AttackRandomRange.Item1, self.AttackRandomRange.Item2);
                    self.CurrentStats.Damages = (int)((self.BaseStats.Damages * 5) * ((10 - numCharacter) / 10.0f));
                    damage = listOfCharacter[indexCharacter].Character.Launchdefense(self, attackJet);
                    if(damage > 0)
                    {
                        self.MyLog($"{self.Name} envoi chaine d'éclair sur {listOfCharacter[indexCharacter].Character.Name} les dommages sont de {self.CurrentStats.Damages}");
                        listOfCharacter.RemoveAt(indexCharacter);
                        numCharacter++;
                    } else
                    {
                        self.MyLog($"{listOfCharacter[indexCharacter].Character.Name} résite à ElectricChain et le sort est interrompu");
                    }
                }
            } while (damage > 0);
            self.CurrentStats.Damages = self.BaseStats.Damages;
        }
    }
}
