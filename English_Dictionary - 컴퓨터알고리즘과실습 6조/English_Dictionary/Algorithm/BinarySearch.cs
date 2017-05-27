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
            public BinarySearch bsearchInstance;

            public int Compare(object x, object y)
            {
                Word wx = (Word)x;
                Word wy = (Word)y;

                //return String.CompareOrdinal(wx.key, wy.key);
                return bsearchInstance.StringCompare(wx.key, wy.key);
            }
        }

        public override int Init(DictionaryReader rd)
        {
            ReverseComparer reverseComparer = new ReverseComparer();
            reverseComparer.bsearchInstance = this;

            reader = rd;

            words = new Word[rd.words.Length];

            Array.Copy(rd.words, words, rd.words.Length);
            Array.Sort(words, reverseComparer);

            return initCount = compareCount;
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

                int compare = StringCompare(searchQuery, words[mid].key);

                if (compare == SAME)
                {
                    queryData.definition = words[mid].definition;
                    break;
                }
                else if(compare == SMALL)
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
