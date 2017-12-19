using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeStringsCounter
{
   public struct WordsSet
    {
        public List<string> UsingWords { get; set; }
        public int[] WordsIndexes { get; set; }

        public WordsSet(List<string> usingWords, List<string> allWords)
        {
            usingWords.Sort(new StringLengthComparer());
            UsingWords = usingWords;

            WordsIndexes = new int[usingWords.Count];
            for (int i = 0; i < WordsIndexes.Length; i++)
            {
                WordsIndexes[i] = allWords.IndexOf(usingWords[i]);
            }
        }
    }
}
