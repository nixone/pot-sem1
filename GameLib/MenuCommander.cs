using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameLib
{
    /// <summary>
    /// Commander specificly implemented to navigate around multiple games.
    /// Supports creating new game, loading new one, and managing high scores
    /// if high score storage was supplied.
    /// </summary>
    public abstract class MenuCommander : Commander
    {
        private IHighScoreStorage _highScoreStorage;

        /// <summary>
        /// Creates new menu commander with optional high score storage
        /// </summary>
        /// <param name="highScoreStorage">high score storage (optional)</param>
        public MenuCommander(IHighScoreStorage highScoreStorage)
        {
            Add(new GameLib.Command("create", "Creates and starts new game"), CreateGame);
            Add(new GameLib.Command("load", "Loads and continues existing game").WithParameter("saveName"), LoadGame);

            if (highScoreStorage != null)
            {
                _highScoreStorage = highScoreStorage;
                Add(new GameLib.Command("highscore", "Shows top 10 of scores"), ShowHighScore);
            }
        }

        private void CreateGame(Command command, String [] parameters)
        {
            Game game = CreateNewGame();
            GameCommander commander = CreateGameCommander(game);
            GameResult result = commander.Play();
            
            if (result != null)
            {
                HandleGameResult(result);
            }
        }

        private void LoadGame(Command command, String [] parameters)
        {
            XmlSerializer serializer = new XmlSerializer(GameType());
            StreamReader reader = new StreamReader(parameters[0] + ".xml");
            Game game = (Game)serializer.Deserialize(reader);
            reader.Close();

            GameCommander commander = CreateGameCommander(game);
            GameResult result = commander.Play();
            if (result != null)
            {
                HandleGameResult(result);
            }

            Console.WriteLine("You are now back in menu. Use 'help' command for options.");
        }

        private void ShowHighScore(Command command, String [] parameters)
        {
            if (_highScoreStorage != null)
            {
                IList<GameResult> topTen = _highScoreStorage.GetTopTen();
                int position = 1;

                foreach (GameResult result in topTen)
                {
                    Console.WriteLine(position + ". " + result.PlayerName + " with score " + result.Score + ". Game was started " + result.StartTime + " and finished " + result.StopTime + " at " + result.MachineName + ".");
                    position++;
                }
            }
        }

        private void HandleGameResult(GameResult result)
        {
            if (_highScoreStorage != null)
            {
                _highScoreStorage.SaveResult(result);
                Console.WriteLine("Your score was marked in the database.");
            }
        }

        /// <summary>
        /// Create new game commander for a specified game. Ment to be overriden if necessary.
        /// </summary>
        /// <param name="game">game</param>
        /// <returns>game commander</returns>
        public virtual GameCommander CreateGameCommander(Game game)
        {
            return new GameCommander(game);
        }

        /// <summary>
        /// Creates new game. Needs to be implemented.
        /// </summary>
        /// <returns></returns>
        public abstract Game CreateNewGame();

        /// <summary>
        /// Get the type of the game this commander handles. Needs to be the type of the game
        /// created in CreateNewGame()
        /// </summary>
        /// <returns></returns>
        public abstract Type GameType();
    }
}
