using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class NamedIdentifiable : AbstractIdentifiable
    {
        public String _name;
        
        public NamedIdentifiable()
        {

        }

        public NamedIdentifiable(String id, String name) : base(id)
        {
            _name = name;
        }

        public override string ToString()
        {
            return GetId() + ": " + _name;
        }
    }
}
