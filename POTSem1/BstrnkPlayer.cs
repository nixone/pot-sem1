using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    public class BstrnkPlayer : GameLib.ConsoleMenuPlayer
    {
        public override Game CreateNewGame()
        {
            return GameOfBstrnk.createNew();
        }
    }
}
