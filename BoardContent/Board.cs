using DevoirMaison2021.Character;
using DevoirMaison2021.Character.CharacterDefinition.SpecialCharacters;
using DevoirMaison2021.Score;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DevoirMaison2021.BoardContent
{
    class Board
    {
        public Random Random { get; }
        public List<BoardCharacter> BoardCharacters { get; set; } = new List<BoardCharacter>();
        Stopwatch watch = new Stopwatch();
        public int NumberBoard { get; }
        public ScoreBoard Score;
        public bool ShowLog { get; set; } = false;
        public Board(int num, ScoreBoard _scoreManager)
        {
            this.NumberBoard = num;
            this.Score = _scoreManager;
            this.Random = new Random(Seed() + (int)DateTime.Now.Ticks);
        }

        public void AddCharacter(AbstractCharacter _character)
        {
            _character.Board = this;
            _character.EventDeadHandler += EventCharacterDead;
            BoardCharacter sc = new BoardCharacter(_character);
            BoardCharacters.Add(sc);
        }
        public void AddCharacterList(List<AbstractCharacter> charactersList)
        {
            charactersList.ForEach(c => AddCharacter(c));
        }
        private BoardCharacter GetBoardCharacter(AbstractCharacter demandeur = null, bool _isDead = false, bool _isEat = false)
        {
            List<BoardCharacter> cs = demandeur == null ? BoardCharacters.FindAll(c => c.IsDead == _isDead && c.IsEat == _isEat && c.Character.IsHidden == false)
                : BoardCharacters.FindAll(c => c.IsDead == _isDead && c.IsEat == _isEat && c.Character.IsHidden == false && c.Character != demandeur);
            if (cs.Count > 0)
            {
                int index = Random.Next(cs.Count);
                return cs[index];
            }
            return null;
        }
        public BoardCharacter GetRandomDeadCharacterNoEat(AbstractCharacter demandeur = null)
        {
            return GetBoardCharacter(demandeur, true, false);
        }

        public BoardCharacter GetRandomCharacterNoDead(AbstractCharacter demandeur = null)
        {
            return GetBoardCharacter(demandeur);
        }
        public async Task Loop()
        {
            bool endBattle = false;
            long deltaTime = 0;
            while (!endBattle)
            {
                watch.Start();
                List<BoardCharacter> copyBoardCharacters = new List<BoardCharacter>(BoardCharacters);
                foreach (BoardCharacter boardCharacter in copyBoardCharacters)
                {
                    // test if Character is not Dead
                    if (!boardCharacter.IsDead)
                    {
                        boardCharacter.Character.LaunchAction(deltaTime);
                    }
                }
                List<BoardCharacter> charactersRestant = BoardCharacters.FindAll(bc => bc.IsDead == false && !(bc.Character is Illusion));
                if (charactersRestant.Count < 2)
                {
                    Console.WriteLine($"Le vainqueur du Battle royale {NumberBoard} est {charactersRestant[0].Character.Name}");
                    Score.PointToDeath(charactersRestant[0].Character.Name);
                    endBattle = true;
                }
                await Task.Delay(1);
                watch.Stop();
                deltaTime = watch.ElapsedMilliseconds;
                watch.Reset();
            }
        }

        public void EventCharacterDead(object sender, EventArgs e)
        {
            BoardCharacter character = BoardCharacters.Find(c => c.Character == sender);
            if (character != null)
            {
                character.IsDead = true;
                MyLog($"­­┼┼┼ {character.Character.Name} est mort");
                if (character.Character is Illusioniste)
                {
                    // Le personnage mort est un Illusioniste, ces Illusions sont détruites à sa mort
                    BoardCharacters.RemoveAll(bc => bc.Character is Illusion illusion && illusion.Parent == character.Character);
                }
                // le mort est une illusion on l'enlève de la liste des personnages
                if (character.Character is AbstractNoPersistCharacter)
                {
                    BoardCharacters.Remove(character);
                }
                else // le mort n'est pas une illusion on l'ajoute au score et on prévient tous les personnages
                {
                    Score.PointToDeath(character.Character.Name);
                    BoardCharacters.FindAll(c => !c.IsDead).ForEach(bc =>
                        bc.Character.OtherCharacterIsDead(character.Character)
                    );
                }
            }
        }
        public void MyLog(string text)
        {
            if (ShowLog)
            {
                Console.WriteLine(text);
            }
        }
        public int Seed(string name = "New random Seed")
        {
            int result = 0;
            foreach (char c in name)
            {
                result += c + NumberBoard;
            }
            return result;
        }
    }
}
