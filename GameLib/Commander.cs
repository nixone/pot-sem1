using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Representation of a console machine that processes users input according to supplied
    /// command definitions and calls specified executors to handle the command calls.
    /// 
    /// Automatically implements commands for exit and help.
    /// </summary>
    public abstract class Commander
    {
        private const String COMMAND_HELP = "help";
        private const String COMMAND_EXIT = "exit";

        /// <summary>
        /// Definition of a callback for a command call
        /// </summary>
        /// <param name="command">command definition which invoked the executor</param>
        /// <param name="parameters">parameters from command line (without command call itself)</param>
        public delegate void Executor(Command command, String[] parameters);

        private bool _isPlaying = false;

        private Dictionary<String, Command> _commandsByString = new Dictionary<String, Command>();
        private Dictionary<String, Executor> _executorsByString = new Dictionary<string, Executor>();

        /// <summary>
        /// Creates default commander with help and exit commands.
        /// </summary>
        public Commander()
        {
            Add(new Command("help", "Prints this help"), ExecuteHelp);
            Add(new Command("exit", "Exits"), ExecuteExit);
        }

        /// <summary>
        /// Adds a new command definition and links an executor for calls on it
        /// </summary>
        /// <param name="command">command definition</param>
        /// <param name="executor">exeturor to invoke on the command</param>
        public void Add(Command command, Executor executor)
        {
            if (_commandsByString.ContainsKey(command.Name))
            {
                throw new ArgumentException("There are more commands with this name");
            }
            _commandsByString.Add(command.Name, command);
            _executorsByString.Add(command.Name, executor);
        }

        /// <summary>
        /// Gives control to this Commander. This method call returns only when the commander
        /// revokes the control on it's own (for example by calling exit).
        /// </summary>
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
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Handles the specified command which was pre-parsed from command line
        /// </summary>
        /// <param name="commandName">name of command called</param>
        /// <param name="parameters">parameters</param>
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

        /// <summary>
        /// Tells the commander to not accept any more commands. This functionality
        /// is affected by ShouldTakeAnotherCommand method, which may be overriden
        /// </summary>
        public void Stop()
        {
            _isPlaying = false;
        }

        /// <summary>
        /// Decides if the commander should accept any more commands or should exit.
        /// Default implementation is affected by Stop() method, which is called on exit command by default.
        /// </summary>
        /// <returns></returns>
        protected virtual bool ShouldTakeAnotherCommand()
        {
            return _isPlaying;
        }
        
        private void ExecuteExit(Command command, String[] parameters)
        {
            Stop();
            Console.WriteLine("You are now in a menu above.");
        }

        private void ExecuteHelp(Command command, String[] parameters)
        {
            Console.WriteLine("Available commands:");
            foreach (Command cmd in _commandsByString.Values)
            {
                Console.WriteLine("\t" + cmd.HelpString);
            }
        }

        /// <summary>
        /// Title of the commander displayed when commander takes control.
        /// </summary>
        /// <returns>title</returns>
        public abstract String GetTitle();
    }
}
