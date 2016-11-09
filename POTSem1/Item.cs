using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    public class Item : NamedIdentifiable, IItem {

        public String _roomIdToBeIn;
        public int _compromisingEuroCost;

        public Item(String id, String name, String roomIdToBeIn, int compromisingEuroCost) : base(id, name)
        {
            _roomIdToBeIn = roomIdToBeIn;
            _compromisingEuroCost = compromisingEuroCost;
        }

        public void Use(Game game)
        {
            game.Get<Room>(_roomIdToBeIn)._hasItem = false;
        }
    }
}
