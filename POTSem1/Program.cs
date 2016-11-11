using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;

namespace POTSem1
{
    class Program
    {
        static void Main(string[] args)
        {
            new BstrnkCommander(new BstrnkHighScoreStorage()).TakeControl();
        }
    }
}
