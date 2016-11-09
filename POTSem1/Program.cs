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
            GameOfBstrnk game = GameOfBstrnk.createNew();
            ConsoleGamePlayer consolePlayer = new ConsoleGamePlayer(game);
            consolePlayer.Play();
        }
    }
}
