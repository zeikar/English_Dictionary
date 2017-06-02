using System.Collections.Generic;
using Algorithm;
using System;
using System.Windows.Forms;

namespace English_Dictionary.Algorithm
{
    public class Trie : AlgorithmWrapper
    {
        class Node
        {
            public int[] children;
            public bool isEnd;
            public string definition;

            public Node()
            {
                children = new int[58];

                for (int i = 0; i < children.Length; i++)
                {
                    children[i] = -1;
                }

                isEnd = false;
            }
        }

        List<Node> trie;

        private int MakeNode()
        {
            trie.Add(new Node());
            return trie.Count - 1;
        }

        private int GetChildIdx(char ch)
        {
            if (ch >= 'a' && ch <= 'z')
            {
                return ch - 'a';
            }
            else if (ch >= 'A' && ch <= 'Z')
            {
                return ch - 'A' + 26;
            }
            else if (ch == ' ')
            {
                return 52;
            }
            else if (ch == '.')
            {
                return 53;
            }
            else if(ch == '\'')
            {
                return 54;
            }
            else if(ch == '-')
            {
                return 55;
            }
            else if (ch == '!')
            {
                return 56;
            }
            else if (ch == '/')
            {
                return 57;
            }
            else
            {
                MessageBox.Show(ch + "");
                return 0;
            }
        }

        private void Add(string key, string definition)
        {
            int lastNode = 0;

            for (int i = 0; i < key.Length; i++)
            {
                if (trie[lastNode].children[GetChildIdx(key[i])] == -1)
                {
                    trie[lastNode].children[GetChildIdx(key[i])] = MakeNode();
                }

                lastNode = trie[lastNode].children[GetChildIdx(key[i])];
            }

            trie[lastNode].isEnd = true;
            trie[lastNode].definition = definition;
        }

        public override int Init(DictionaryReader rd)
        {
            reader = rd;

            trie = new List<Node>();
            trie.Add(new Node());

            for (int i = 0; i < rd.words.Length; i++)
            {
                Add(reader.words[i].key, rd.words[i].definition);
            }

            return 0;
        }

        public override QueryData Search(string searchQuery)
        {
            QueryData queryData = new QueryData();
            compareCount = 0;

            int lastNode = 0, i;

            for (i = 0; i < searchQuery.Length; i++)
            {
                if (trie[lastNode].children[GetChildIdx(searchQuery[i])] == -1)
                {
                    break;
                }

                lastNode = trie[lastNode].children[GetChildIdx(searchQuery[i])];
            }

            if (i == searchQuery.Length)
            {
                queryData.definition = trie[lastNode].definition;
            }

            queryData.compareCount = compareCount;
            return queryData;
        }
    }
}
