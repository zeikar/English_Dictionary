using Algorithm;
using System;

namespace English_Dictionary.Algorithm
{
    abstract public class AlgorithmWrapper
    {
        // string 비교 함수
        protected const int SMALL = -1;
        protected const int SAME = 0;
        protected const int LARGE = 1;

        protected int StringCompare(string str1, string str2)
        {
            for (int i = 0; i < Math.Max(str1.Length, str2.Length); i++)
            {
                ++compareCount;

                // 길이가 다르면 더 긴 쪽이 큰 걸로 간주
                if (i >= str1.Length || i >= str2.Length)
                {
                    if (str1.Length > str2.Length)
                    {
                        return LARGE;
                    }
                    else
                    {
                        return SMALL;
                    }
                }

                if (str1[i] < str2[i])
                {
                    return SMALL;
                }
                else if (str1[i] > str2[i])
                {
                    return LARGE;
                }
            }

            return SAME;
        }

        // 비교 횟수 변수
        protected int compareCount, initCount;

        protected DictionaryReader reader;

        abstract public QueryData Search(string searchQuery);

        abstract public int Init(DictionaryReader rd);
    }
}