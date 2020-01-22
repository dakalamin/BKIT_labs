using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    public struct MinMax
    {
        public int Min;
        public int Max;

        public MinMax(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
    public static class SubArrays
    {
        public static List<MinMax> DivideSubArrays(int begin, int end, int subArraysCount)
        {
            List<MinMax> result = new List<MinMax>();

            if ((end - begin) <= subArraysCount)
            {
                result.Add(new MinMax(0, (end - begin)));
            }
            else if (subArraysCount > 0)
            {
                int delta = (end - begin) / subArraysCount;
                int currentBegin = begin;

                while ((end - currentBegin) >= 2 * delta)
                {
                    result.Add(new MinMax(currentBegin, currentBegin + delta));
                    currentBegin += delta;
                }
                result.Add(new MinMax(currentBegin, end));
            }

            return result;
        }
    }
}
