using GameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POTSem1
{
    public class BstrnkHighScoreStorage : SqlHighScoreStorage
    {

        public static String ObtainConnectionString()
        {
            Console.Write("Connecting to database, please enter owners student number: ");
            return "Data Source=sharp.kst.fri.uniza.sk;Initial Catalog=potst2016_OlesnanikMartinDb;Persist Security Info=True;User ID=potst2016_OlesnanikMartin;Password=" + Console.ReadLine();
        }

        public BstrnkHighScoreStorage() : base(ObtainConnectionString())
        {

        }
    }
}
