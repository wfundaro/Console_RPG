using System;
using System.Collections.Generic;

namespace DevoirMaison2021.Score
{
    class ScoreManager
    {
        public List<ScoreBoard> Score = new List<ScoreBoard>();
        /// <summary>
        /// Ajoute un ScoreBoard au gestionnaire de score
        /// </summary>
        /// <param name="numBoard"></param>
        /// <returns></returns>
        public ScoreBoard AddScoreBoard(int numBoard)
        {
            ScoreBoard newScoreBoard = new ScoreBoard();
            if(numBoard >= Score.Count)
            {
                Score.Add(newScoreBoard);
            } else
            {
                Score[numBoard] = newScoreBoard;
            }
            return newScoreBoard;
        }
        /// <summary>
        /// Récupère le ScoreBoard à l'index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ScoreBoard GetScoreBoardAt(int index)
        {
            if(index < Score.Count)
            {
                return Score[index];
            }
            return null;
        }
        /// <summary>
        /// Affiche tous les scores des battles ou ceux demandés passés en paramètres
        /// </summary>
        /// <param name="numBoards"></param>
        public void ShowScores(params int[] numBoards) // paramètres optionnels pour afficher les scores des boards sélectionnés
        {
            ScoreBoard totalScores = new ScoreBoard();
            if(numBoards.Length == 0)
            {
                for (int i = 0; i < Score.Count; i++)
                {
                    ShowScoreBoard(ref totalScores, i);
                }
            } else
            {
                for (int i = 0; i < numBoards.Length; i++)
                {
                    ShowScoreBoard(ref totalScores, numBoards[i]);
                }
            }
            Console.WriteLine("\nTotal ----------------------------------------------------");
            totalScores.ShowSortScore();
        }
        /// <summary>
        /// Affiche les scores du board demandé par la méthode ShowScores
        /// </summary>
        /// <param name="totalScores"></param>
        /// <param name="indexScoreBoard"></param>
        private void ShowScoreBoard(ref ScoreBoard totalScores, int indexScoreBoard)
        {
            if(indexScoreBoard >= Score.Count)
            {
                Console.WriteLine($"Le board numéro {indexScoreBoard} n'existe pas");
                return;
            }
            Console.WriteLine($"\nRésultat du board numéro {indexScoreBoard}");
            Score[indexScoreBoard].ShowScores();
            Console.WriteLine();
            totalScores += Score[indexScoreBoard];
        }
    }
}
