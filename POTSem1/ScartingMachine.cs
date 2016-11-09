using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    class ScartingMachine : NamedIdentifiable, GameLib.IItem
    {
        public const String ID = "scarting-machine";
        const String NAME = "Scarting machine, ends the game";

        public ScartingMachine() : base(ID, NAME)
        {
        }

        public void Use(Game game)
        {
            game.Finish();
        }
    }
}
