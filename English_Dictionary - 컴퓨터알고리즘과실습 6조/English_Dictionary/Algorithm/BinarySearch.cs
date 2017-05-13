using System;
using Algorithm;
using System.Collections;

namespace English_Dictionary.Algorithm
{
    public class BinarySearch : AlgorithmWrapper
    {
        Word[] words;

        // class for sorting
        public class ReverseComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                Word wx = (Word)x;
                Word wy = (Word)y;

                return String.CompareOrdinal(wx.key, wy.key);
            }
        }

        public override void Init(DictionaryReader rd)
        {
            reader = rd;

            words = new Word[rd.words.Length];

            Array.Copy(rd.words, words, rd.words.Length);
            Array.Sort(words, new ReverseComparer());
        }

        public override QueryData Search(string searchQuery)
        {
            QueryData queryData = new QueryData();
            compareCount = 0;
            
            // binary search
            int high = words.Length - 1, low = 0;

            while (high >= low)
            {
                int mid = (high + low) / 2;

                int compare = StringCompare(searchQuery, words[mid].key, true);

                if (compare == SAME)
                {
                    queryData.definition = words[mid].definition;
                    break;
                }
                else if(StringCompare(searchQuery, words[mid].key) == SMALL)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            queryData.compareCount = compareCount;
            return queryData;
        }
    }
}
