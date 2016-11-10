using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public abstract class Commander
    {
        private const String COMMAND_HELP = "help";
        private const String COMMAND_EXIT = "exit";

        public delegate void Executor(Command command, String[] parameters);

        private bool _isPlaying = false;

        private Dictionary<String, Command> _commandsByString = new Dictionary<String, Command>();
        private Dictionary<String, Executor> _executorsByString = new Dictionary<string, Executor>();

        public Commander()
        {
            Add(new Command("help", "Prints this help"), ExecuteHelp);
            Add(new Command("exit", "Exits"), ExecuteExit);
        }

        public void Add(Command command, Executor executor)
        {
            if (_commandsByString.ContainsKey(command.Name))
            {
                throw new ArgumentException("There are more commands with this name");
            }
            _commandsByString.Add(command.Name, command);
            _executorsByString.Add(command.Name, executor);
        }

        public virtual void TakeControl()
        {
            _isPlaying = true;

            Console.WriteLine("----");
            Console.WriteLine(GetTitle());
            Console.WriteLine("----");
            Console.WriteLine();
            Console.WriteLine("(for command list, use 'help')");
            Console.WriteLine();

            while (ShouldTakeAnotherCommand())
            {
                Console.Write("Your command: ");
                String line = Console.ReadLine();
                if (line.Length == 0) continue;
                String[] command = line.Split(' ');
                if (command.Length < 1) continue;
                String commandName = command[0];
                String[] parameters = Utils.subArray<String>(command, 1);

                try
                {
                    ExecuteCommand(commandName, parameters);
                }
                catch (Exception e)
                {
                    Console.WriteLine("There is a problem! " + e.Message);
                    Utils.PrintStackTrace(e);
                }
                Console.WriteLine();
            }
        }

        private void ExecuteCommand(String commandName, String[] parameters)
        {
            if (!_commandsByString.ContainsKey(commandName))
            {
                throw new ArgumentException("Sorry, we do not know this command. Please use '" + COMMAND_HELP + "' for help.");
            }
            Command command = _commandsByString[commandName];
            if (parameters.Length != command.ParameterCount)
            {
                throw new ArgumentException("Sorry, you did provide incorrect number of parameters");
            }
            Executor executor = _executorsByString[commandName];
            executor(command, parameters);
        }

        public void Stop()
        {
            _isPlaying = false;
        }

        protected virtual bool ShouldTakeAnotherCommand()
        {
            return _isPlaying;
        }

        private void ExecuteExit(Command command, String[] parameters)
        {
            Stop();
        }

        private void ExecuteHelp(Command command, String[] parameters)
        {
            Console.WriteLine("Available commands:");
            foreach (Command cmd in _commandsByString.Values)
            {
                Console.WriteLine("\t" + cmd.HelpString);
            }
        }

        public abstract String GetTitle();
    }
}
