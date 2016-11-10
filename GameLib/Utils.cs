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

        public static T[] subArray<T>(T[] array, int startIndex)
        {
            return subArray(array, startIndex, array.Length-startIndex);
        }

        public static T[] subArray<T>(T[] array, int startIndex, int length)
        {
            if (length-startIndex > array.Length)
            {
                throw new ArgumentException("not enough data for this startIndex and length");
            }
            T[] result = new T[length];
            for (int i=startIndex,j=0; i<startIndex+length; i++,j++)
            {
                result[j] = array[i];
            }
            return result;
        }
    }
}
