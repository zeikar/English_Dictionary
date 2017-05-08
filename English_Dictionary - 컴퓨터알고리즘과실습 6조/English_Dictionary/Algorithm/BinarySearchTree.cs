using System;
using Algorithm;

namespace English_Dictionary.Algorithm
{
    public class BinarySearchTree : AlgorithmWrapper
    {
        // node
        class Node
        {
            public Word word;
            public Node left, right;

            public Node(Word w)
            {
                word = w;
                left = null;
                right = null;
            }
        }
        Node header;

        public override void Init(DictionaryReader rd)
        {
            reader = rd;

            header = new Node(new Word("A", "A"));

            for (int i = 0; i < rd.words.Length; i++)
            {
                Add(rd.words[i]);
            }
        }

        public override QueryData Search(string searchQuery)
        {
            QueryData queryData = new QueryData();
            compareCount = 0;

            Node parent = header;
            Node ptr = header.right;

            while (ptr != null)
            {
                int compare = StringCompare(searchQuery, ptr.word.key, true);

                if (compare == SAME)
                {
                    queryData.definition = ptr.word.definition;
                    break;
                }
                else if(compare == SMALL)
                {
                    ptr = ptr.left;
                }
                else
                {
                    ptr = ptr.right;
                }
            }

            queryData.compareCount = compareCount;
            return queryData;
        }
        
        private void Add(Word word)
        {
            string insert = word.key;

            Node parent = header;
            Node ptr = header.right;

            while (ptr != null)
            {
                parent = ptr;

                // // 값이 더 작으면 왼쪽, 아니면 오른쪽 자식으로 이동
                if (StringCompare(insert, ptr.word.key) == SMALL)
                {
                    ptr = ptr.left;
                }
                else
                {
                    ptr = ptr.right;
                }
            }

            Node newNode = new Node(word);

            // // 값이 더 작으면 왼쪽, 아니면 오른쪽 자식에 대입
            if (StringCompare(insert, parent.word.key) == SMALL)
            {
                parent.left = newNode;
            }
            else
            {
                parent.right = newNode;
            }
        }
    }
}
