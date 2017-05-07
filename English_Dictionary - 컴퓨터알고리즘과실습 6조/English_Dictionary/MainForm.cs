using Algorithm;
using English_Dictionary.Algorithm;
using System;
using System.Windows.Forms;

namespace English_Dictionary
{
    public partial class MainForm : Form
    {
        DictionaryReader reader;

        public MainForm()
        {
            InitializeComponent();

            reader = new DictionaryReader();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchQuery = searchTextBox.Text;

            AlgorithmWrapper[] algorithm = new AlgorithmWrapper[2];

            // ----- 0. Brute Force -----
            BruteForce bruteForce = new BruteForce();
            bruteForce.Init(reader);
            // --------------------------

            algorithm[0] = bruteForce;

            QueryData ret = algorithm[0].Search(searchQuery);

            if (ret.definition == null)
            {
                ret.definition = "해당하는 단어가 없습니다.";

                MessageBox.Show("해당하는 단어가 없습니다.");
            }

            resultLabel.Text = ret.definition;
            countLabel.Text = "문자 비교 횟수 : " + ret.compareCount;
        }
    }
}