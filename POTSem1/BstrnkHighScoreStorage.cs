using GameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POTSem1
{
    /// <summary>
    /// Specific implementation of high score storage for BSTRNK game
    /// </summary>
    public class BstrnkHighScoreStorage : SqlHighScoreStorage
    {
        /// <summary>
        /// Obtains the connection string based on definition and asks the user through the console for a database password
        /// </summary>
        /// <returns>full connection string</returns>
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
