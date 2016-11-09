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

        public void Play()
        {
            _isPlaying = true;

            Console.WriteLine("--------");
            Console.WriteLine(_game.GetTitle());
            Console.WriteLine("--------");
            Console.WriteLine();
            Console.WriteLine("---- Story");
            Console.WriteLine(_game.GetStory());
            Console.WriteLine();
            Console.WriteLine("---- Objective");
            Console.WriteLine(_game.GetObjective());
            Console.WriteLine();
            Console.WriteLine("(for help, write '"+COMMAND_HELP+"' and learn more)");
            Console.WriteLine();

            while (_isPlaying)
            {
                Console.Write("Your command: ");
                String line = Console.ReadLine();
                if (line.Length == 0) continue;
                String [] command = line.Split(' ');
                try
                {
                    ExecuteCommand(command);
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("There is a problem! " + e.Message);
                }
                
            }
        }

        private void ExecuteCommand(String[] command)
        {
            switch (command[0])
            {
                case COMMAND_EXIT: ExecuteExit(); break;
                case COMMAND_HELP: ExecuteHelp(); break;
                default: throw new ArgumentException("Sorry, we don't know this command. Use '"+COMMAND_HELP+"' for help");
            }
        }

        private void ExecuteExit()
        {
            Stop();
        }

        private void ExecuteHelp()
        {
            Console.WriteLine("These are avialable commands:");
            Console.WriteLine("\t" + COMMAND_EXIT + ": Exits the game");
            Console.WriteLine("\t" + COMMAND_HELP + ": Prints this help");
        }
    }
}
