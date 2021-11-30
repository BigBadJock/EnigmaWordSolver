using EnigmaWordSolver.Contracts;
using EnigmaWordSolver.DTO;
using EnigmaWordSolver.miscellaneous;
using System.Text;
using System.Text.RegularExpressions;

namespace EnigmaWordSolver
{
    public class EnigmaWord : IEnigmaWord
    {
        Dictionary<int, string> givenValues = null;
        List<string> wordList = null;
        private readonly IWordListLoader wordListLoader;

        public EnigmaWord(IWordListLoader wordListLoader)
        {
            this.givenValues = new Dictionary<int, string>();
            this.PossibleValues = new Dictionary<int, string>();
            this.WordsToSolve = new List<WordToSolve>();
            this.wordListLoader = wordListLoader ?? throw new ArgumentNullException(nameof(wordListLoader));

            this.wordList = wordListLoader.LoadWords();
        }


        public Dictionary<int, string> GivenValues
        {
            get { return givenValues; }
        }

        public Dictionary<int, string> PossibleValues { get; }


        private List<string> LoadWordList()
        {
            /// Load list of words from text file
            if (!File.Exists("words.txt"))
            {
                throw new Exception("Words File does not exist");
            }

            var path = "words.txt";

            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            string line = String.Empty;

            List<string> wordList = new List<string>();

            while ((line = sr.ReadLine()) != null)
            {
                wordList.Add(line.Trim());
            }

            return wordList;
        }

        public List<WordToSolve> WordsToSolve { get; }

        public bool AddWord(string codedWord)
        {
            if (string.IsNullOrWhiteSpace(codedWord)) throw new ArgumentException("Coded Word Cannot be empty");
            if (codedWord.IndexOf(",") < 0) throw new NotCommaSeparatedException();
            if (codedWord.IndexOf(",") == 0) throw new WordCannotBeginWithCommaException();
            if (codedWord.IndexOf(",,") > -1) throw new WordCannotHaveDoubleCommasException();

            string[] letters = codedWord.Split(',');
            letters.ToList().ForEach(x =>
           {
               try
               {
                   int y = int.Parse(x);
                   if (y < 1 || y > 26) throw new WordCannotInvalidNumberException();
               }
               catch (ArgumentException ex)
               {
                   throw new WordCannotInvalidNumberException();
               }

           });


            this.WordsToSolve.Add(new WordToSolve(codedWord));
            return true;
        }

        public void AddGivenValue(int number, string value)
        {
            if (number < 1 || number > 26) throw new GivenValueInvalidException();
            if (value.Length > 1) throw new GivenValueInvalidException();
            value = value.ToLowerInvariant();

            Match match = Regex.Match(value, "[a-z]");
            if (!match.Success) throw new GivenValueInvalidException();

            if (givenValues.ContainsKey(number)) throw new GivenValueInvalidException();
            if (givenValues.ContainsValue(value)) throw new GivenValueInvalidException();

            this.givenValues.Add(number, value);
        }

        public async Task<bool> Solve()
        {
            if (this.WordsToSolve.Count == 0) throw new NoWordsException();

            CalculatePossibleValues();


            Solver solver = new Solver(givenValues, PossibleValues, wordList, WordsToSolve);

            bool result = await solver.Solve();

            this.givenValues = solver.GivenValues;

            return result;
        }

        private void CalculatePossibleValues()
        {
            string s = string.Empty;
            string availableLetters = "abcdefghijklmnopqrstuvwxyz";
            this.PossibleValues.Clear();

            givenValues.Values.ToList().ForEach(x =>
            {
                if (availableLetters.Contains(x))
                {
                    availableLetters = availableLetters.Substring(0, availableLetters.IndexOf(x)) + availableLetters.Substring(availableLetters.IndexOf(x) + 1);
                }
            });



            for (int i = 1; i < 27; i++)
            {
                if (!givenValues.ContainsKey(i))
                {
                    PossibleValues.Add(i, availableLetters);
                }
            }
        }
    }
}
