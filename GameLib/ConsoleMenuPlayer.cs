using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameLib
{
    public abstract class ConsoleMenuPlayer
    {
        const String COMMAND_CREATE = "create";
        const String COMMAND_LOAD = "load";
        const String COMMAND_EXIT = "exit";

        private bool _isRunning = false;

        public void Play()
        {
            _isRunning = true;

            while (_isRunning)
            {
                Console.WriteLine("Welcome in game menu:");
                Console.WriteLine("\t" + COMMAND_CREATE + ": Creates and starts new game");
                Console.WriteLine("\t" + COMMAND_LOAD + " {name}: Loads and continues saved game");
                Console.WriteLine("\t" + COMMAND_EXIT + ": Quits the program");
                Console.WriteLine();

                try
                {
                    Console.Write("Enter your option: ");
                    String[] command = Console.ReadLine().Split(' ');
                    switch (command[0])
                    {
                        case COMMAND_CREATE: ExecuteCreate(); break;
                        case COMMAND_LOAD: ExecuteLoad(command); break;
                        case COMMAND_EXIT: ExecuteExit(); break;
                        default: throw new ArgumentException("Unknown command");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops, there was a problem: " + e.Message);
                }
                Console.WriteLine();
            }
        }

        private void ExecuteCreate()
        {
            Game game = CreateNewGame();
            ConsoleGamePlayer player = new ConsoleGamePlayer(game);
            player.Play();
        }

        private void ExecuteLoad(String[] command)
        {
            if (command.Count() < 2)
            {
                throw new ArgumentException("missing an argument");
            }

            XmlSerializer serializer = new XmlSerializer(GameType());
            StreamReader reader = new StreamReader(command[1]+".xml");
            Game game = (Game)serializer.Deserialize(reader);
            reader.Close();

            ConsoleGamePlayer player = new ConsoleGamePlayer(game);
            player.Play();
        }

        private void ExecuteExit()
        {
            _isRunning = false;
        }

        public abstract Type GameType();

        public abstract Game CreateNewGame();
    }
}
