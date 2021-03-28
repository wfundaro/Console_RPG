using DevoirMaison2021.BoardContent;
using DevoirMaison2021.Character;
using DevoirMaison2021.Character.CharacterDefinition;
using DevoirMaison2021.Score;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DevoirMaison2021
{
    class Program
    {
        // Mettre SHOW_LOG à true pour voir l'ensemble des coups des personnages
        public static bool SHOW_LOG = true;
        // Définie le nombre de battle qui seront lancées
        public static int NB_BATTLE = 1;
        public static ScoreManager scoreManager;
        static readonly CancellationTokenSource cancelTknSrc = new CancellationTokenSource();
        static async Task Main(string[] args)
        {
            try
            {
                await LaunchBattles(NB_BATTLE, cancelTknSrc.Token);
            }
            catch (OperationCanceledException)
            {
                ShowInterrupBattle();
            }
        }
        /// <summary>
        /// Lance un nombre de battles définies
        /// </summary>
        /// <param name="nb"></param>
        /// <param name="_cancelTkn"></param>
        /// <returns></returns>
        static async Task LaunchBattles(int nb, CancellationToken _cancelTkn)
        {
            scoreManager = new ScoreManager();
            Task cancel = Task.Run(() => BattleCancellation());
            List<Task> taches = new List<Task>();
            for (int i = 0; i < nb; i++)
            {
                scoreManager.AddScoreBoard(i);
                taches.Add(LaunchBoard(i, scoreManager.GetScoreBoardAt(i), SHOW_LOG));
            }
            await Task.Run(() =>
            {
                try
                {
                    Task.WaitAll(taches.ToArray(), _cancelTkn);
                    scoreManager.ShowScores();
                }
                catch (OperationCanceledException)
                {
                    ShowInterrupBattle();
                }
            }, _cancelTkn);

        }
        /// <summary>
        /// Crée un Board qui représente une battle
        /// </summary>
        /// <param name="num"></param>
        /// <param name="_scoreBoard"></param>
        /// <param name="showlog"></param>
        /// <returns></returns>
        static async Task LaunchBoard(int num, ScoreBoard _scoreBoard, bool showlog)
        {
            // On crée un plateau
            Board board = new Board(num, _scoreBoard) { ShowLog = showlog };
            // On ajoute les personnages
            AbstractCharacter guerrier = new Guerrier();
            AbstractCharacter paladin = new Paladin();
            AbstractCharacter berseker = new Berseker();
            AbstractCharacter zombie = new Zombie();
            AbstractCharacter robot = new Robot();
            AbstractCharacter vampire = new Vampire();
            AbstractCharacter pretre = new Pretre();
            AbstractCharacter magicien = new Magicien();
            AbstractCharacter illusioniste = new Illusioniste();
            AbstractCharacter alchimiste = new Alchimiste();
            AbstractCharacter assassin = new Assassin();
            AbstractCharacter necromancien = new Necromancien();

            board.AddCharacterList(new List<AbstractCharacter> { guerrier, paladin, berseker, zombie, robot, vampire, pretre, magicien, illusioniste, alchimiste, assassin, necromancien });
            await board.Loop();
        }
        /// <summary>
        /// Permet de stopper les battles et d'appeler la fin du programme en appyant sur ESC
        /// </summary>
        /// <returns></returns>
        private static async Task BattleCancellation()
        {
            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                // Press escape for stop Battle
                await Task.Delay(1);
            }
            cancelTknSrc.Cancel();
        }
        /// <summary>
        /// Message affiché si interruption des battles
        /// </summary>
        private static void ShowInterrupBattle()
        {
            Console.WriteLine(" Suite à une épidémie Mystérieuse les battles ont été interrompues");
        }
    }
}
