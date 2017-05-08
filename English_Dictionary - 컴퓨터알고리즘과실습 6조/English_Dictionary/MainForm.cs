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
        const int ALGORITHM_NUM = 2;
        readonly string[] algorithmNames = { "Brute Force", "Binary Search Tree", "Red Black Tree", "Hashing" };

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
            // ----- 1. Binary Search Tree -----
            algorithm[1] = new BinarySearchTree();

            for (int i = 0; i < ALGORITHM_NUM; i++)
            {
                searchButton.Text = algorithmNames[i] + " 초기화 하는 중...잠시만 기다리세요...";
                algorithm[i].Init(reader);
                initProgressBar.PerformStep();
            }

            infoLabel.Text = "검색할 단어를 입력하세요";
            searchButton.Text = "검색";
            searchButton.Enabled = true;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (initialized == false)
            {
                ThreadPool.QueueUserWorkItem(Init);
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

            // 한국어 뜻 출력
            if (ret[0].definition == null)
            {
                ret[0].definition = "해당하는 단어가 없습니다.";

                MessageBox.Show("해당하는 단어가 없습니다.");
            }
            resultLabel.Text = ret[0].definition;

            // 비교횟수 출력
            countLabel.Text = "";
            for (int i = 0; i < ALGORITHM_NUM; i++)
            {
                countLabel.Text += algorithmNames[i] + " 문자 비교 횟수 : " + ret[i].compareCount + '\n';
            }
        }
    }
}