using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    public class Room : NamedIdentifiable, GameLib.IRoom
    {
        internal String _itemIdToHave;
        internal bool _hasItem;

        public Room(String id, String name) : base(id, name)
        {
            _hasItem = false;
            _itemIdToHave = null;
        }

        public Room(String id, String name, String itemIdToHave) : base(id, name)
        {
            _itemIdToHave = itemIdToHave;
            _hasItem = true;
        }

        public ISet<IItem> GetItems(Game game)
        {
            HashSet<Item> items = new HashSet<Item>();
            if (_hasItem)
            {
                items.Add(game.Get<Item>(_itemIdToHave));
            }
            return (ISet<IItem>)items;
        }

        public void WalkIn(Game game, IPassage throughPassage, IRoom fromRoom)
        {
            // Nothing to do actually
        }
    }
}
