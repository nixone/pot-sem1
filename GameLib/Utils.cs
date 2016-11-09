using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class Utils
    { 
        public static void PrintStackTrace(Exception e)
        {
            while (e != null)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
               
                e = e.InnerException;
            }
        }
    }
}
