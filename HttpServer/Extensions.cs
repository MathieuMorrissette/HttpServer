using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public static class Extensions
    {
        public static T[] SubArray<T>(this T[] array, int start)
        {
            return array.SubArray(start, array.Length - start);
        }

        public static T[] SubArray<T>(this T[] array, int start, int length)
        {
            length = Math.Max(length, 0);
            T[] result = new T[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = array[i + start];
            }

            return result;
        }
    }
}