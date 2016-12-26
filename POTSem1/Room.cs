using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    /// <summary>
    /// Specific implementation of Room for BSTRNK game
    /// </summary>
    public class Room : NamedIdentifiable, IRoom
    {
        public String _itemIdToHave;
        public bool _hasItem;

        /// <summary>
        /// Construtor not to be used in code. For serialization purposes only.
        /// </summary>
        public Room()
        {

        }

        /// <summary>
        /// Construtor to be used in code. This constructor creates a room without an item
        /// </summary>
        /// <param name="id">room identifier</param>
        /// <param name="name">name / description of room</param>
        public Room(String id, String name) : base(id, name)
        {
            _hasItem = false;
            _itemIdToHave = null;
        }

        /// <summary>
        /// Construtor to be used in code. This constructor creates a room with an item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="itemIdToHave"></param>
        public Room(String id, String name, String itemIdToHave) : base(id, name)
        {
            _itemIdToHave = itemIdToHave;
            _hasItem = true;
        }

        public ISet<IItem> GetItems(Game game)
        {
            HashSet<IItem> items = new HashSet<IItem>();
            if (_hasItem)
            {
                items.Add(game.Get<IItem>(_itemIdToHave));
            }
            return items;
        }

        public void WalkIn(Game game, IPassage throughPassage, IRoom fromRoom)
        {
            // Nothing to do actually
        }
    }
}
