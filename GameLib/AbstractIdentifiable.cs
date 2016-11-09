using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class AbstractIdentifiable : IIdentifiable
    {
        private String _id;

        public AbstractIdentifiable(String id)
        {
            _id = id;
        }

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
