using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    /// <summary>
    /// Specific implementation of Passage for BSTRNK game
    /// Passages have some cost to go through and defined room ids on both ends
    /// </summary>
    public class Passage : AbstractIdentifiable, IPassage
    {
        public int _euroCost;
        public String _fromRoomId;
        public String _toRoomId;

        /// <summary>
        /// Construtor not to be used in code. For serialization purposes only.
        /// </summary>
        public Passage()
        {

        }

        /// <summary>
        /// Constructor to be used from code
        /// </summary>
        /// <param name="id">passage id</param>
        /// <param name="euroCost">cost in euros to use the passage</param>
        /// <param name="fromRoomId">room id on one end</param>
        /// <param name="toRoomId">room id on other end</param>
        public Passage(String id, int euroCost, String fromRoomId, String toRoomId) : base(id)
        {
            _euroCost = euroCost;
            _fromRoomId = fromRoomId;
            _toRoomId = toRoomId;
        }

        public IRoom OneEnd(Game game)
        {
            return game.Get<IRoom>(_fromRoomId);
        }

        public IRoom SecondEnd(Game game)
        {
            return game.Get<IRoom>(_toRoomId);
        }

        public void Use(Game game)
        {
            BstrnkGame myGame = (BstrnkGame)game;
            myGame._eurosSpent += _euroCost;
        }

        public override string ToString()
        {
            return "Passage between " + _fromRoomId + " and " + _toRoomId + " for " + _euroCost + " euros";
        }
    }
}
