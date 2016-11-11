using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameLib
{
    public abstract class MenuCommander : Commander
    {
        private IHighScoreStorage _highScoreStorage;

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

        public void CreateGame(Command command, String [] parameters)
        {
            Game game = CreateNewGame();
            GameCommander commander = CreateGameCommander(game);
            GameResult result = commander.Play();
            
            if (result != null)
            {
                HandleGameResult(result);
            }
        }

        public void LoadGame(Command command, String [] parameters)
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

        public void ShowHighScore(Command command, String [] parameters)
        {
            if (_highScoreStorage != null)
            {
                IList<GameResult> topTen = _highScoreStorage.GetTopTen();
                int position = 1;

                foreach (GameResult result in topTen)
                {
                    Console.WriteLine(position + ". " + result.PlayerName + " with score " + result.Score);
                    position++;
                }
            }
        }

        public void HandleGameResult(GameResult result)
        {
            if (_highScoreStorage != null)
            {
                _highScoreStorage.SaveResult(result);
                Console.WriteLine("Your score was marked in the database.");
            }
        }

        public virtual GameCommander CreateGameCommander(Game game)
        {
            return new GameCommander(game);
        }

        public abstract Game CreateNewGame();

        public abstract Type GameType();
    }
}
