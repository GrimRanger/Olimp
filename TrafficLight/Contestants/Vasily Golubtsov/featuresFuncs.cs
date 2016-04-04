using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vasily_Golubtsov
{
    static class featuresFuncs
    {
        public static T[] GetRow<T>(this T[,] mx, int i)
        {
            T[] row = new T[mx.GetLength(1)];
            for (int j = 0; j < row.Length; j++)
            {
                row[j] = mx[i, j];
            }
            return row;
        }

        public static void Set<T>(this T[] row, T[] newValue)
        {
            for (int i = 0; i < row.Length; i++)
                row[i] = newValue[i];
        }
    }
}
