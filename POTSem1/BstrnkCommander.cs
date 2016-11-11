using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    public class BstrnkCommander : MenuCommander
    {
        public BstrnkCommander(IHighScoreStorage highScoreStorage) : base(highScoreStorage)
        {

        }

        public override Game CreateNewGame()
        {
            return BstrnkGame.createNew();
        }

        public override Type GameType()
        {
            return typeof(BstrnkGame);
        }

        public override string GetTitle()
        {
            return "Menu of game BSTRNK";
        }
    }
}
