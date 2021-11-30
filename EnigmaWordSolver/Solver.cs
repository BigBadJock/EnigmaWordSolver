using EnigmaWordSolver.Contracts;
using EnigmaWordSolver.DTO;
using System.Text.RegularExpressions;

namespace EnigmaWordSolver
{
    public class Solver : ISolver
    {
        private Dictionary<int, string> givenValues;
        private Dictionary<int, string> possibleValues;

        private List<string> wordList;
        private List<WordToSolve> wordsToSolve;

        public Solver(Dictionary<int, string> givenValues, Dictionary<int, string> possibleValues, List<string> wordList, List<WordToSolve> wordsToSolve)
        {
            this.givenValues = givenValues ?? throw new ArgumentNullException(nameof(givenValues));
            this.possibleValues = possibleValues ?? throw new ArgumentNullException(nameof(possibleValues));
            this.wordList = wordList ?? throw new ArgumentNullException(nameof(wordList));
            this.wordsToSolve = wordsToSolve ?? throw new ArgumentNullException(nameof(wordsToSolve));
        }

        public async Task<bool> Solve()
        {
            wordsToSolve.ForEach(wordToSolve =>
            {
                string word = GetWordWithWildcards(wordToSolve);

                wordToSolve.IsMatched = IsMatchedWord(word);
                if (!wordToSolve.IsMatched)
                {
                    wordToSolve.PossibleMatches = CountPossibleMatches(word);
                }
            });

            int solvedWords = wordsToSolve.Where(x => x.IsMatched).Count();
            int unsolveableWords = wordsToSolve.Where(x => !x.IsMatched && x.PossibleMatches < 1).Count();
            bool result = result = solvedWords == wordsToSolve.Count;

            if (solvedWords < wordsToSolve.Count && unsolveableWords == 0)
            {
                result = await GetNextIteration();
            }

            return result;
        }

        private bool IsMatchedWord(string word)
        {
            return !word.Contains("?") && wordList.Contains(word);
        }

        private int CountPossibleMatches(string wordWithWildcards)
        {
            string pattern = WildCardToRegular(wordWithWildcards);
            int result = wordList
                .Where(word => word.Length == wordWithWildcards.Length && Regex.IsMatch(word, pattern))
                .Count();
            return result;
        }

        private string GetWordWithWildcards(WordToSolve wordToSolve)
        {
            List<string> digits = wordToSolve.CodedWord.Split(',').ToList();
            string word = string.Empty;

            digits.ForEach(x =>
            {
                int i = int.Parse(x);
                if (givenValues.ContainsKey(i))
                {
                    word += givenValues[i];
                }
                else
                {
                    word += "?";
                }
            });
            return word;
        }

        private Tuple<int, string> GetPossibleValues(WordToSolve wordToSolve)
        {
            List<string> digits = wordToSolve.CodedWord.Split(',').ToList();
            string wordWithWildcards = GetWordWithWildcards(wordToSolve);
            int i = wordWithWildcards.IndexOf('?');
            int key = int.Parse(digits[i]);

            string pattern = WildCardToRegular(wordWithWildcards);
            var matches = wordList.Where(word => word.Length == wordWithWildcards.Length && Regex.IsMatch(word, pattern)).ToList();

            string possible = string.Empty;
            matches.ForEach(match =>
            {
                string s = match.Substring(i, 1);
                if (!possible.Contains(s))
                {
                    possible += s;
                }
            });

            return new Tuple<int, string>(key, possible);
        }



        // TODO - this loop isn't working correctly.
        // TODO - can get possible matches which gives us possible letters - this should narrow down possible choices - optimisation
        // TODO - can we find doubles and eliminate/demote unlikely double letters
        // TODO - can we calculate frequency of digits and prioritise with most frequent letters
        private async Task<bool> GetNextIteration()
        {
            Dictionary<int, string> clonedGivenValues = givenValues.ToDictionary(entry => entry.Key, entry => entry.Value);
            Dictionary<int, string> clonedPossibleValues = possibleValues.ToDictionary(entry => entry.Key, entry => entry.Value);
            bool result = false;
            bool canContinue = true;
            while (canContinue)
            {
                canContinue = false;


                wordsToSolve = wordsToSolve.OrderBy(x => x.PossibleMatches).ToList();
                WordToSolve wordToSolve = wordsToSolve.Where(wordToSolve => !wordToSolve.IsMatched && wordToSolve.PossibleMatches > 0).First();
                Tuple<int, string> possible = GetPossibleValues(wordToSolve);
                if (possible.Item2.Length > 0)
                {
                    clonedGivenValues.Add(possible.Item1, possible.Item2);
                    clonedPossibleValues.Remove(possible.Item1);
                    canContinue = true;
                }

                if (canContinue)
                {
                    Solver solver = new Solver(clonedGivenValues, clonedPossibleValues, wordList, wordsToSolve);
                    result = await solver.Solve();
                    int unsolveableWords = wordsToSolve.Where(x => !x.IsMatched && x.PossibleMatches < 1).Count();

                    if (result || unsolveableWords == 0) canContinue = false;
                }
                else
                {
                    return false;
                }
            }
            return result;
        }

        private static String WildCardToRegular(String value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }
    }
}
