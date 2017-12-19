using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeStringsCounter
{
    public class StringLengthComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            if (s1.Length > s2.Length)
            {
                return 1;
            }
            else if (s2.Length > s1.Length)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
