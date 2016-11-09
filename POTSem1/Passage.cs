using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    public class Passage : AbstractIdentifiable, IPassage
    {
        public int _euroCost;
        public String _fromRoomId;
        public String _toRoomId;

        public Passage()
        {

        }

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
            GameOfBstrnk myGame = (GameOfBstrnk)game;
            myGame._eurosSpent += _euroCost;
        }

        public override string ToString()
        {
            return "Passage between " + _fromRoomId + " and " + _toRoomId + " for " + _euroCost + " euros";
        }
    }
}
