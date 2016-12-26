using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Representation of console command with name, description and parameter names.
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Name of the command used to specify it from the command line
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Description of the command, mainly for help
        /// </summary>
        public String Description { get; private set; }

        /// <summary>
        /// Number of parameters required by this command
        /// </summary>
        public int ParameterCount
        {
            get
            {
                return _parameters.Count;
            }
        }

        /// <summary>
        /// Formatted help string for this command
        /// </summary>
        public String HelpString
        {
            get
            {
                String output = Name;
                foreach (String parameter in _parameters)
                {
                    output += " {" + parameter + "}";
                }
                output += ": " + Description;
                return output;
            }
        }

        private List<String> _parameters = new List<String>();

        /// <summary>
        /// Constructs new command definition
        /// </summary>
        /// <param name="name">name for command line</param>
        /// <param name="description">description for help</param>
        public Command(String name, String description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Adds a new parameter definition to the command
        /// </summary>
        /// <param name="parameterName">name of parameter (for help)</param>
        /// <returns></returns>
        public Command WithParameter(String parameterName)
        {
            _parameters.Add(parameterName);
            return this;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (obj == null) return false;
            if (obj.GetType() != this.GetType()) return false;
            Command c = (Command)obj;
            return c.Name.Equals(Name);
        }
    }
}
