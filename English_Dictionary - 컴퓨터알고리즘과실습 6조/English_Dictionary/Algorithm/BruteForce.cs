using Algorithm;
using System;

namespace English_Dictionary.Algorithm
{
    public class BruteForce : AlgorithmWrapper
    {
        public override int Init(DictionaryReader rd)
        {
            reader = rd;

            return 0;
        }

        public override QueryData Search(string searchQuery)
        {
            QueryData queryData = new QueryData();
            compareCount = 0;

            for (int i = 0; i < reader.words.Length; i++)
            {
                if(StringCompare(searchQuery, reader.words[i].key) == SAME)
                {
                    queryData.definition = reader.words[i].definition;
                    break;
                }
            }

            queryData.compareCount = compareCount;
            return queryData;
        }
    }
}