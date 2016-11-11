using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    /// <summary>
    /// Utility methods not belonging to any other specific class
    /// </summary>
    public class Utils
    { 
        /// <summary>
        /// Prints whole stack trace (also with inner exceptions) to console.
        /// Useful for debugging purposes without really stopping your program.
        /// </summary>
        /// <param name="e">top level exception</param>
        public static void PrintStackTrace(Exception e)
        {
            while (e != null)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
               
                e = e.InnerException;
            }
        }

        /// <summary>
        /// Creates a sub array from original array. Sub array items will start from an index of original array
        /// </summary>
        /// <typeparam name="T">type of array items</typeparam>
        /// <param name="array">original items</param>
        /// <param name="startIndex">index to start from</param>
        /// <returns>new sub array</returns>
        public static T[] subArray<T>(T[] array, int startIndex)
        {
            return subArray(array, startIndex, array.Length-startIndex);
        }

        /// <summary>
        /// Creates a sub array from original array of a specified length. Sub array items will start from an index of original array
        /// </summary>
        /// <typeparam name="T">type of array items</typeparam>
        /// <param name="array">orignial items</param>
        /// <param name="startIndex">index to start from</param>
        /// <param name="length">number of items to take</param>
        /// <returns>new sub array</returns>
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
