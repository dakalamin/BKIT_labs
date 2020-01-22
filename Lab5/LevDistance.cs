using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public static class LevDistance
    {
        private static int _tripleMin(int a, int b, int c) => Math.Min(a, Math.Min(b, c));

        private static int _basicDistance(string s1, string s2, bool flag)
        {
            int len1 = s1.Length;
            int len2 = s2.Length;
            if (len1 == 0 || len2 == 0)
                return Math.Max(len1, len2);

            string tmp1 = s1.ToUpper();
            string tmp2 = s2.ToUpper();

            int[,] d = new int[len1 + 1, len2 + 1];
            for (int i = 0; i <= len1; ++i) d[i, 0] = i;
            for (int j = 1; j <= len2; ++j) d[0, j] = j;

            for (int i = 1; i <= len1; ++i)
                for (int j = 1; j <= len2; ++j)
                {
                    int indr = (tmp1[i - 1] == tmp2[j - 1]) ? 0 : 1;

                    d[i, j] = flag ?
                        _tripleMin(d[i, j - 1] + 1,
                                   d[i - 1, j] + 1,
                                   d[i - 1, j - 1] + indr)
                        :
                        _tripleMin(d[i - 1, j] + 1,
                                   d[i, j - 1] + 1,
                                   d[i - 1, j - 1] + indr);

                    if (flag && (i > 1) && (j > 1) && (tmp1[i - 1] == tmp2[j - 2]) && (tmp1[i - 2] == tmp2[j - 1]))
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + indr);
                }
            return d[len1, len2];
        }

        public static int Distance(string s1, string s2)
        {
            return _basicDistance(s1, s2, false);
        }

        public static int DistanceDameray(string s1, string s2)
        {
            return _basicDistance(s1, s2, true);
        }
    }
}
