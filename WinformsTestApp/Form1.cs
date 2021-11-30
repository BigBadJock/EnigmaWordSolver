using EnigmaWordSolver;

namespace WinformsTestApp
{
    public partial class Form1 : Form
    {
        private readonly EnigmaWord enigmaWord;

        public Form1(EnigmaWord enigmaWord)
        {
            InitializeComponent();
            this.enigmaWord = enigmaWord ?? throw new ArgumentNullException(nameof(enigmaWord));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string key = textBox2.Text;
            string value = textBox3.Text;

            enigmaWord.GivenValues.Add(int.Parse(key), value);
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            enigmaWord.WordsToSolve.Clear();

            List<string> words = textBox1.Lines.ToList();
            words.ForEach(x =>
            {
                enigmaWord.AddWord(x);
            });

            enigmaWord.Solve();



        }
    }
}