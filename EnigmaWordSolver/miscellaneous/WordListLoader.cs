using EnigmaWordSolver.Contracts;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace EnigmaWordSolver.miscellaneous
{
    public class WordListLoader : IWordListLoader
    {
        private readonly IConfiguration configuration;

        public WordListLoader(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public List<string> LoadWords()
        {
            string path = configuration["WordFilePath"];
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));

            if (!File.Exists("words.txt"))
            {
                throw new Exception("Words File does not exist");
            }

            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            string line = String.Empty;

            List<string> wordList = new List<string>();

            while ((line = sr.ReadLine()) != null)
            {
                wordList.Add(line.Trim().ToLowerInvariant());
            }

            return wordList;
        }
    }
}
