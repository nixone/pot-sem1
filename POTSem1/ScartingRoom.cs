using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    public class ScartingRoom : NamedIdentifiable, GameLib.IRoom
    {
        public const String ID = "scarting-room";
        const String NAME = "Scarting room, where everything disappears";

        public ScartingRoom() : base(ID, NAME)
        {
        }

        public ISet<IItem> GetItems(Game game)
        {
            HashSet<IItem> result = new HashSet<IItem>();
            result.Add(game.Get<ScartingMachine>(ScartingMachine.ID));
            return result;
        }

        public void WalkIn(Game game, IPassage throughPassage, IRoom fromRoom)
        {
            // Nothing to do here
        }
    }
}
