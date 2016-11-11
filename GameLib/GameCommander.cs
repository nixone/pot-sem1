using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameLib
{
    public class GameCommander : Commander
    {
        private Game _game;

        public GameCommander(Game game)
        {
            _game = game;

            Add(new Command("story", "Prints the story of the game"), ExecuteStory);
            Add(new Command("objective", "Prints objective of the game"), ExecuteObjective);
            Add(new Command("discover", "Discovers where are you and what you see"), ExecuteDiscover);
            Add(new Command("goto", "Goes to the specified room").WithParameter("room"), ExecuteGoto);
            Add(new Command("use", "Uses the item / takes it into inventory").WithParameter("item"), ExecuteUse);
            Add(new Command("save", "Saves current state of the game").WithParameter("saveName"), ExecuteSave);
        }

        public GameResult Play()
        {
            base.TakeControl();

            if (!_game.IsRunning())
            {
                int score = _game.GetScore();
                Console.WriteLine("Hooray! You finished the game with total score of " + score + "!");
                Console.Write("Please enter your name: ");
                String name = Console.ReadLine();
                return new GameResult(name, score);
            }
            return null;
        }

        protected override bool ShouldTakeAnotherCommand()
        {
            return base.ShouldTakeAnotherCommand() && _game.IsRunning();
        }

        private void ExecuteStory(Command command, String [] parameters)
        {
            Console.WriteLine(_game.GetStory());
        }

        public void ExecuteObjective(Command command, String[] parameters)
        {
            Console.WriteLine(_game.GetObjective());
        }

        public void ExecuteDiscover(Command command, String[] parameters)
        {
            Console.WriteLine("Game: " + _game);
            Console.WriteLine("Current room: " + _game.GetCurrentRoom());
            Console.WriteLine("Current score: " + _game.GetScore());
            Console.WriteLine();

            ISet<IItem> items = _game.GetCurrentRoom().GetItems(_game);
            Console.WriteLine("You see " + items.Count + " items");
            foreach (IItem item in items)
            {
                Console.WriteLine("\t" + item);
            }
            Console.WriteLine();

            ISet<IRoom> roomsToGoTo = _game.GetRoomsToWalkTo(_game.GetCurrentRoom());
            Console.WriteLine("You can go to " + roomsToGoTo.Count + " rooms from here");
            foreach (IRoom room in roomsToGoTo)
            {
                Console.WriteLine("\t" + room);
            }
            Console.WriteLine();
        }

        public void ExecuteGoto(Command command, String[] parameters)
        {
            _game.GoInto(parameters[0]);
            Console.WriteLine("Now you are in " + _game.GetCurrentRoom());
        }

        public void ExecuteUse(Command command, String[] parameters)
        {
            IItem item = _game.Get<IItem>(parameters[0]);
            item.Use(_game);
            Console.WriteLine("You did use " + item);
        }

        public void ExecuteSave(Command command, String[] parameters)
        {
            XmlSerializer ser = new XmlSerializer(_game.GetType());
            TextWriter writer = new StreamWriter(parameters[0] + ".xml");
            ser.Serialize(writer, _game);
            writer.Close();
            Console.WriteLine("Game was saved");
        }

        public override string GetTitle()
        {
            return _game.GetTitle();
        }
    }
}
