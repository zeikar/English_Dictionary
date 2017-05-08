using Algorithm;
using System;

namespace English_Dictionary.Algorithm
{
    public class BruteForce : AlgorithmWrapper
    {
        public override void Init(DictionaryReader rd)
        {
            reader = rd;
        }

        public override QueryData Search(string searchQuery)
        {
            QueryData queryData = new QueryData();
            compareCount = 0;

            for (int i = 0; i < reader.words.Length; i++)
            {
                if(StringCompare(searchQuery, reader.words[i].key, true) == SAME)
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