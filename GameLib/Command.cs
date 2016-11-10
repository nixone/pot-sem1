using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class Command
    {
        public String Name { get; private set; }
        public String Description { get; private set; }

        public int ParameterCount
        {
            get
            {
                return _parameters.Count;
            }
        }

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

        public Command(String name, String description)
        {
            Name = name;
            Description = description;
        }

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
