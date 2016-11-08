using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public abstract class AbstractIdentifiable : IIdentifiable
    {
        public abstract String GetId();

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
