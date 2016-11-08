using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public abstract class Game
    {
        private Dictionary<String, IIdentifiable> _identifiables = new Dictionary<string, IIdentifiable>();

        public IIdentifiable GetIdentifiable(String key)
        {
            if (!_identifiables.ContainsKey(key))
            {
                throw new IndexOutOfRangeException(key);
            }
            return _identifiables[key];
        }

        public T GetIdentifiable<T>(String key)
        {
            IIdentifiable identifiable = GetIdentifiable(key);
            if (!(identifiable is T))
            {
                throw new IndexOutOfRangeException(key + " is not correct type");
            }
            return (T)identifiable;
        }

        public void Initialize()
        {

        }

        public abstract int GetScore();
    }
}
