using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Description of object with a string identifier
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Get the identifier value
        /// </summary>
        /// <returns>identifier value</returns>
        String GetId();
    }
}
