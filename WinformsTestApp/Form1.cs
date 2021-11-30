using EnigmaWordSolver;
using System.Text;

namespace WinformsTestApp
{
    public partial class Form1 : Form
    {
        private readonly IEnigmaWord enigmaWord;

        public Form1(IEnigmaWord enigmaWord)
        {
            InitializeComponent();
            this.enigmaWord = enigmaWord ?? throw new ArgumentNullException(nameof(enigmaWord));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text;
            string value = txtValue.Text;

            enigmaWord.GivenValues.Add(int.Parse(key), value);

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            enigmaWord.GivenValues.ToList().ForEach(x =>
            {
                sb.AppendLine($"{x.Key}-{x.Value}");
            });
            txtGivenValues.Text = sb.ToString();


        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            enigmaWord.WordsToSolve.Clear();

            List<string> words = txtWordsToSolve.Lines.ToList();
            words.ForEach(x =>
            {
                if (!string.IsNullOrWhiteSpace(x)) enigmaWord.AddWord(x);
            });

            var result = enigmaWord.Solve().Result;

            if (result)
            {
                StringBuilder sb = new StringBuilder();

                enigmaWord.WordsToSolve.ForEach(x =>
                {
                    sb.AppendLine(x.UncodedWord);
                });

                txtResults.Text = sb.ToString();

                sb.Clear();
                enigmaWord.GivenValues.ToList().ForEach(x =>
                {
                    sb.AppendLine($"{x.Key}-{x.Value}");
                });
                txtGivenValues.Text = sb.ToString();
            }



        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            enigmaWord.WordsToSolve.Clear();
            enigmaWord.GivenValues.Clear();
            txtGivenValues.Text = "";
            txtWordsToSolve.Text = "";

        }
    }
}