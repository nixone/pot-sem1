using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class ConsoleGamePlayer
    {
        const String COMMAND_EXIT = "exit";
        const String COMMAND_HELP = "help";
        const String COMMAND_GO_TO = "goto";
        const String COMMAND_DISCOVER = "discover";
        const String COMMAND_USE = "use";
        const String COMMAND_STORY = "story";
        const String COMMAND_OBJECTIVE = "objective";

        private Game _game;
        private bool _isPlaying = false;

        public ConsoleGamePlayer(Game game)
        {
            _game = game;
        }

        public void Stop()
        {
            _isPlaying = false;
        }

        public GameResult Play()
        {
            _isPlaying = true;

            Console.WriteLine("--------");
            Console.WriteLine(_game.GetTitle());
            Console.WriteLine("--------");
            Console.WriteLine();
            ExecuteStory();
            Console.WriteLine();
            ExecuteObjective();
            Console.WriteLine();
            Console.WriteLine("(for help, write '" + COMMAND_HELP + "' and learn more)");
            Console.WriteLine();

            while (_isPlaying && _game.IsRunning())
            {
                Console.Write("Game command: ");
                String line = Console.ReadLine();
                if (line.Length == 0) continue;
                String[] command = line.Split(' ');
                try
                {
                    ExecuteCommand(command);
                }
                catch (Exception e)
                {
                    Console.WriteLine("There is a problem! " + e.Message);
                    Console.WriteLine(e.StackTrace);
                }
                Console.WriteLine();
            }

            // If we finished the game
            if (_isPlaying && !_game.IsRunning())
            {
                int score = _game.GetScore();
                Console.WriteLine("Hooray! You finished the game with total score of " + score + "!");
                Console.Write("Please enter your name: ");
                String name = Console.ReadLine();
                return new GameResult(name, score);
            }

            return null;
        }

        private void ExecuteCommand(String[] command)
        {
            switch (command[0])
            {
                case COMMAND_EXIT: ExecuteExit(); break;
                case COMMAND_HELP: ExecuteHelp(); break;
                case COMMAND_STORY: ExecuteStory(); break;
                case COMMAND_OBJECTIVE: ExecuteObjective(); break;
                case COMMAND_DISCOVER: ExecuteDiscover(); break;
                case COMMAND_GO_TO: ExecuteGoto(command); break;
                case COMMAND_USE: ExecuteUse(command); break;
                default: throw new ArgumentException("Sorry, we don't know this command. Use '" + COMMAND_HELP + "' for help");
            }
        }

        private void ExecuteExit()
        {
            Stop();
        }

        private void ExecuteHelp()
        {
            Console.WriteLine("These are avialable commands:");
            Console.WriteLine("\t" + COMMAND_EXIT + ": Quits this game");
            Console.WriteLine("\t" + COMMAND_HELP + ": Prints this help");
            Console.WriteLine("\t" + COMMAND_STORY + ": Prints story of the game");
            Console.WriteLine("\t" + COMMAND_OBJECTIVE + ": Prints objective of the game");
            Console.WriteLine("\t" + COMMAND_DISCOVER + ": Discovers where are you and what you see");
            Console.WriteLine("\t" + COMMAND_GO_TO + " {room}: Goes to the specified room");
            Console.WriteLine("\t" + COMMAND_USE + " {item}: Uses the specified item");
        }

        private void ExecuteStory()
        {
            Console.WriteLine("---- Story");
            Console.WriteLine(_game.GetStory());
        }

        private void ExecuteObjective()
        {
            Console.WriteLine("---- Objective");
            Console.WriteLine(_game.GetObjective());
        }

        private void ExecuteDiscover()
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
                Console.WriteLine("\t"+room);
            }
            Console.WriteLine();
        }

        private void ExecuteGoto(String[] command)
        {
            if (command.Length < 1)
            {
                throw new ArgumentException("You have to provide second parameter");
            }
            _game.GoInto(command[1]);
            Console.WriteLine("Now you are in "+_game.GetCurrentRoom());
        }

        private void ExecuteUse(String[] command)
        {
            if (command.Length < 1)
            {
                throw new ArgumentException("You have to provide second parameter");
            }
            IItem item = _game.Get<IItem>(command[1]);
            item.Use(_game);
            Console.WriteLine("You did use " + item);
        }
    }
}
