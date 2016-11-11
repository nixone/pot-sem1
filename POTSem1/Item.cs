using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    /// <summary>
    /// Specific item impelmentation for BSTRNK game
    /// These items have one designated room to start from and compromising cost for the player
    /// </summary>
    public class Item : NamedIdentifiable, IItem {

        public String _roomIdToBeIn;
        public int _compromisingEuroCost;

        public Item()
        {

        }

        public Item(String id, String name, String roomIdToBeIn, int compromisingEuroCost) : base(id, name)
        {
            _roomIdToBeIn = roomIdToBeIn;
            _compromisingEuroCost = compromisingEuroCost;
        }

        public void Use(Game game)
        {
            // Tells the room that it doesnt have the item anymore
            game.Get<Room>(_roomIdToBeIn)._hasItem = false;
        }
    }
}
