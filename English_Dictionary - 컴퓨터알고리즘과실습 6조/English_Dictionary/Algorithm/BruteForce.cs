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

            for (int i = 0; i < reader.words.Length; i++)
            {
                bool isFound = true;

                for (int j = 0; j < Math.Max(reader.words[i].key.Length, searchQuery.Length); j++)
                {
                    ++queryData.compareCount;

                    if (j >= reader.words[i].key.Length ||
                        j >= searchQuery.Length ||
                        searchQuery[j] != reader.words[i].key[j])
                    {
                        isFound = false;
                        break;
                    }
                }

                if (isFound)
                {
                    queryData.definition = reader.words[i].definition;
                    return queryData;
                }
            }

            queryData.definition = null;
            return queryData;
        }
    }
}