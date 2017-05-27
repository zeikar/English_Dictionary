using System;
using Algorithm;
using System.Collections.Generic;

namespace English_Dictionary.Algorithm
{
    public class Hashing : AlgorithmWrapper
    {
        List<Word>[] hashTable;
        const int HASH_TABLE_SIZE = 10007;

        public override int Init(DictionaryReader rd)
        {
            reader = rd;

            hashTable = new List<Word>[HASH_TABLE_SIZE];

            for (int i = 0; i < HASH_TABLE_SIZE; i++)
            {
                hashTable[i] = new List<Word>();
            }

            for (int i = 0; i < rd.words.Length; i++)
            {
                int hash = Hash(rd.words[i].key);

                hashTable[hash].Add(rd.words[i]);
            }

            return 0;
        }

        public override QueryData Search(string searchQuery)
        {
            QueryData queryData = new QueryData();
            compareCount = 0;

            int hash = Hash(searchQuery);

            for (int i = 0; i < hashTable[hash].Count; i++)
            {
                if (StringCompare(searchQuery, hashTable[hash][i].key) == SAME)
                {
                    queryData.definition = hashTable[hash][i].definition;
                    break;
                }
            }

            queryData.compareCount = compareCount;
            return queryData;
        }

        private int Hash(string str)
        {
            int hash = 0;

            // 소문자로 바꾸고 해시 값 계산
            string insert = "";

            for (int i = 0; i < str.Length; i++)
            {
                // 대문자는 소문자로
                if (str[i] >= 'A' && str[i] <= 'Z')
                {
                    insert += (char)(str[i] - 'A' + 'a');
                }
                // 소문자는 그대로
                else if (str[i] >= 'a' && str[i] <= 'z')
                {
                    insert += str[i];
                }
            }

            // 라빈 카프 알고리즘 응용
            const int d = 26;

            for (int i = 0; i < insert.Length; i++)
            {
                hash *= d;
                hash += (insert[i] - 'a');
                hash %= HASH_TABLE_SIZE;
            }

            return hash % HASH_TABLE_SIZE;
        }
    }
}
