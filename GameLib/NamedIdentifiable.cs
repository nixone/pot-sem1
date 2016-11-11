using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Identifiable object with a human friendly description
    /// </summary>
    public class NamedIdentifiable : AbstractIdentifiable
    {
        public String _name;
        
        /// <summary>
        /// Constructor not to be used in code. For serialization purposes
        /// </summary>
        public NamedIdentifiable()
        {

        }

        /// <summary>
        /// Constructor to be used in code
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="name">name / description</param>
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
