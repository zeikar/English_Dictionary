using Algorithm;
using English_Dictionary.Algorithm;
using System;
using System.Threading;
using System.Windows.Forms;

namespace English_Dictionary
{
    public partial class MainForm : Form
    {
        DictionaryReader reader;

        bool initialized = false;

        AlgorithmWrapper[] algorithm;
        const int ALGORITHM_NUM = 5;
        readonly string[] algorithmNames = { "Brute Force", "Binary Search", "Binary Search Tree", "Red Black Tree", "Hashing" };

        public MainForm()
        {
            InitializeComponent();
        }

        private void Init(object state)
        {
            searchButton.Enabled = false;
            infoLabel.Text = "초기화 중입니다...기다려 주세요";
            initProgressBar.Step = 100 / ALGORITHM_NUM;

            reader = new DictionaryReader();

            // init algorithms
            algorithm = new AlgorithmWrapper[ALGORITHM_NUM];
            // ----- 0. Brute Force -----
            algorithm[0] = new BruteForce();
            // ----- 1. Binary Search -----
            algorithm[1] = new BinarySearch();
            // ----- 2. Binary Search Tree -----
            algorithm[2] = new BinarySearchTree();
            // ----- 3. Red Black Tree -----
            algorithm[3] = new RedBlackTree();
            // ----- 4. Hashing -----
            algorithm[4] = new Hashing();

            for (int i = 0; i < ALGORITHM_NUM; i++)
            {
                searchButton.Text = algorithmNames[i] + " 초기화 하는 중...잠시만 기다리세요...";
                algorithm[i].Init(reader);
                initProgressBar.PerformStep();
            }
            initProgressBar.Value = 100;

            infoLabel.Text = "검색할 단어를 입력하세요";
            searchButton.Text = "검색";
            searchButton.Enabled = true;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (initialized == false)
            {
                // RELEASE
                ThreadPool.QueueUserWorkItem(Init);

                // DEBUG
                //Init(null);

                initialized = true;
                return;
            }

            string searchQuery = searchTextBox.Text;

            // 탐색 실행
            QueryData[] ret = new QueryData[ALGORITHM_NUM];
            for (int i = 0; i < ALGORITHM_NUM; i++)
            {
                ret[i] = algorithm[i].Search(searchQuery);
            }

            // 출력
            resultLabel.Text = "";
            countLabel.Text = "";
            for (int i = 0; i < ALGORITHM_NUM; i++)
            {
                if (ret[i].definition == null)
                {
                    ret[i].definition = "해당하는 단어가 없습니다.";
                }

                resultLabel.Text += ret[i].definition + "\n\n";
                countLabel.Text += algorithmNames[i] + " 문자 비교 횟수 : " + ret[i].compareCount + "\n\n";
            }
        }
    }
}