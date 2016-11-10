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
        public MenuCommander()
        {
            Add(new GameLib.Command("create", "Creates and starts new game"), CreateGame);
            Add(new GameLib.Command("load", "Loads and continues existing game").WithParameter("saveName"), LoadGame);
        }

        public void CreateGame(Command command, String [] parameters)
        {
            Game game = CreateNewGame();
            GameCommander commander = CreateGameCommander(game);
            commander.Play();
            // TODO Handle game result
        }

        public void LoadGame(Command command, String [] parameters)
        {
            XmlSerializer serializer = new XmlSerializer(GameType());
            StreamReader reader = new StreamReader(parameters[0] + ".xml");
            Game game = (Game)serializer.Deserialize(reader);
            reader.Close();

            GameCommander commander = CreateGameCommander(game);
            commander.Play();
            // TODO Handle game result

            Console.WriteLine("You are now back in menu. Use 'help' command for options.");
        }

        public virtual GameCommander CreateGameCommander(Game game)
        {
            return new GameCommander(game);
        }

        public abstract Game CreateNewGame();

        public abstract Type GameType();
    }
}
