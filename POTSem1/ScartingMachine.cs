using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    /// <summary>
    /// Special scarting machine impelmentation of Item for BSTRNK game
    /// </summary>
    public class ScartingMachine : NamedIdentifiable, GameLib.IItem
    {
        public const String ID = "scarting-machine";
        const String NAME = "Scarting machine, ends the game";
        
        public ScartingMachine() : base(ID, NAME)
        {
        }

        public void Use(Game game)
        {
            // Using scarting machine finishes the game
            game.Finish();
        }
    }
}
