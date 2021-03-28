using System;
using System.Collections.Generic;
using System.Text;

namespace DevoirMaison2021.Score
{
    class ScoreBoard
    {
        struct Perso
        {
            public string Name;
            public int Point;
        }
        private readonly object pointToDeathLock = new object();
        Dictionary<string, int> Scores { get; set; } = new Dictionary<string, int>();
        /// <summary>
        /// Ajoute le personnage au tableau de score et lui attribut les points
        /// </summary>
        /// <param name="name"></param>
        public void PointToDeath(string name)
        {
            lock (pointToDeathLock)
            {
                int point = Scores.Count;
                Scores.Add(name, point);
            }
        }
        /// <summary>
        /// Affiche les scores des personnages 
        /// </summary>
        public void ShowScores()
        {
            foreach (KeyValuePair<string, int> keyValue in Scores)
            {
                Console.Write($"  {keyValue.Key} => {keyValue.Value}  |");
            }
        }
        /// <summary>
        /// Surcharge de l'opérateur + afin de pouvoir faire l'addition de score entre 2 ScoreBoard
        /// </summary>
        /// <param name="sc1"></param>
        /// <param name="sc2"></param>
        /// <returns></returns>
        public static ScoreBoard operator+ (ScoreBoard sc1, ScoreBoard sc2)
        {
            ScoreBoard scoreBoardresult = new ScoreBoard();
            scoreBoardresult.Scores = new Dictionary<string, int>(sc1.Scores);
            foreach (KeyValuePair<string, int> keyValue in sc2.Scores)
            {
                if (scoreBoardresult.Scores.ContainsKey(keyValue.Key))
                {
                    scoreBoardresult.Scores[keyValue.Key] += keyValue.Value;
                } else
                {
                    scoreBoardresult.Scores.Add(keyValue.Key, keyValue.Value);
                }
            }
            return scoreBoardresult;
        }
        /// <summary>
        /// Affichage des score triés du plus petit au plus grand
        /// </summary>
        public void ShowSortScore()
        {
            List<Perso> persoScore = new List<Perso>();
            foreach (KeyValuePair<string, int> keyValue in Scores)
            {
                Perso p = new Perso { Name = keyValue.Key, Point = keyValue.Value};
                persoScore.Add(p);
            }
            persoScore.Sort((a,b) => a.Point.CompareTo(b.Point));
            foreach(Perso scoreOfPerso in persoScore)
            {
                Console.Write($"  {scoreOfPerso.Name} => {scoreOfPerso.Point}  |");
            }
            Console.WriteLine();
        }
    }
}
