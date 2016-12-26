using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Basic implementation of objects with non-changing identifier
    /// </summary>
    public class AbstractIdentifiable : IIdentifiable
    {
        public String _id;

        /// <summary>
        /// Required for serialization, should not be used from code
        /// </summary>
        public AbstractIdentifiable()
        {

        }

        /// <summary>
        /// Usual constructor which sets the id of the object
        /// </summary>
        /// <param name="id"></param>
        public AbstractIdentifiable(String id)
        {
            _id = id;
        }

        /// <summary>
        /// Returns the object's id
        /// </summary>
        /// <returns></returns>
        public String GetId()
        {
            return _id;
        }

        public override int GetHashCode()
        {
            return GetId().GetHashCode();
        }

        public override Boolean Equals(object obj)
        {
            if (obj == null) return false;
            if (!obj.GetType().Equals(GetType())) return false;
            return GetId().Equals(((AbstractIdentifiable)obj).GetId());
        }
    }
}
