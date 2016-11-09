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
        public ScartingMachine(String id) : base(id, "Scarting machine, ends the game")
        {
        }

        public void Use(Game game)
        {

        }
    }
}
