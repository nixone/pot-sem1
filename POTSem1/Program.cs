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
        public static String ObtainConnectionString()
        {
            Console.Write("Connecting to database, please enter owners student number: ");
            return "Data Source=sharp.kst.fri.uniza.sk;Initial Catalog=potst2016_OlesnanikMartinDb;Persist Security Info=True;User ID=potst2016_OlesnanikMartin;Password=" + Console.ReadLine();
        }

        static void Main(string[] args)
        {
            new SqlHighScoreStorage(ObtainConnectionString()).SaveResult(new GameResult("nixone", 0));
            //new BstrnkCommander().TakeControl();
        }
    }
}
