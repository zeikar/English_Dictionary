using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace English_Dictionary.Algorithm
{
    public class RedBlackTree : AlgorithmWrapper
    {
        // color red, black
        const int RED = 1;
        const int BLACK = 0;

        // node
        class Node
        {
            public Word word;
            public Node left, right;
            public int color;

            public Node(Word w, int col, Node dummy)
            {
                word = w;
                color = col;
                left = dummy;
                right = dummy;
            }
        }
        Node header, dummy;


        public override int Init(DictionaryReader rd)
        {
            reader = rd;

            // dummy node
            dummy = new Node(new Word("", ""), BLACK, null);
            dummy.left = dummy;
            dummy.right = dummy;

            header = new Node(new Word("0", "0"), BLACK, dummy);

            for (int i = 0; i < rd.words.Length; i++)
            {
                Insert(rd.words[i]);
            }

            return initCount = compareCount;
        }

        public override QueryData Search(string searchQuery)
        {
            QueryData queryData = new QueryData();
            compareCount = 0;

            Node parent = header;
            Node ptr = header.right;

            while (ptr != dummy)
            {
                int compare = StringCompare(searchQuery, ptr.word.key);

                if (compare == SAME)
                {
                    queryData.definition = ptr.word.definition;
                    break;
                }
                else if (compare == SMALL)
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

        private void Insert(Word word)
        {
            string insert = word.key;
            
            Node parent = header;
            Node grandParent = header;
            Node grandGrandParent = header;
            Node ptr = header;

            while (ptr != dummy)
            {
                grandGrandParent = grandParent;
                grandParent = parent;
                parent = ptr;

                // 값이 더 작으면 왼쪽, 아니면 오른쪽 자식으로 이동
                if (StringCompare(insert, ptr.word.key) == SMALL)
                {
                    ptr = ptr.left;
                }
                else
                {
                    ptr = ptr.right;
                }

                // 만약 두 자식의 색깔이 red면 split해준다.
                if (ptr.left.color == RED && ptr.right.color == RED)
                {
                    Split(grandGrandParent, grandParent, parent, ptr, insert);
                }
            }

            Node newNode = new Node(word, RED, dummy);

            // 값이 더 작으면 왼쪽, 아니면 오른쪽 자식에 대입
            if (StringCompare(insert, parent.word.key) == SMALL)
            {
                parent.left = newNode;
            }
            else
            {
                parent.right = newNode;
            }

            // split 해준다.
            // -> red black tree 성질을 유지하기 위함
            Split(grandGrandParent, grandParent, parent, newNode, insert);
            // 루트는 black
            header.right.color = BLACK;
        }

        private Node Rotate(Node grandParent, string key)
        {
            Node high, low;

            // 부모를 알아낸다.
            if (StringCompare(key, grandParent.word.key) == SMALL)
            {
                high = grandParent.left;
            }
            else
            {
                high = grandParent.right;
            }

            // 자식을 알아낸 후 회전
            // 우회전
            if (StringCompare(key, high.word.key) == SMALL)
            {
                low = high.left;

                high.left = low.right;
                low.right = high;
            }
            // 좌회전
            else
            {
                low = high.right;

                high.right = low.left;
                low.left = high;
            }

            // 조부모와 연결
            if (StringCompare(key, grandParent.word.key) == SMALL)
            {
                grandParent.left = low;
            }
            else
            {
                grandParent.right = low;
            }

            return low;
        }

        private void Split(Node grandGrandParent, Node grandParent, Node parent, Node ptr, string key)
        {
            // 자신은 red로 자식들을 black으로 해준다. -> 분할
            ptr.color = RED;
            ptr.left.color = BLACK;
            ptr.right.color = BLACK;

            // 부모도 red이면 red red 이므로 회전
            if(parent.color == RED)
            {
                // 중간에 꺾여있으면 -> key가 parent, grandparent 사이 값이면 이중회전
                if(StringCompare(key, parent.word.key) != StringCompare(key, grandParent.word.key))
                {
                    Rotate(grandParent, key);
                }

                // 회전했으니까 블랙으로 해준다.
                parent = Rotate(grandGrandParent, key);
                parent.color = BLACK;

                // 한번 회전 했으므로 grandParent는 자식으로 내려갔기 때문에 레드로 해준다.
                grandParent.color = RED;
            }
        }
    }
}