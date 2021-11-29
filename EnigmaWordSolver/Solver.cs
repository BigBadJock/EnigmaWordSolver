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
                List<string> s = wordToSolve.CodedWord.Split(',').ToList();

                string word = string.Empty;

                s.ForEach(x =>
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

                wordToSolve.IsMatched = false;
                if (!word.Contains("?"))
                {
                    if (wordList.Contains(word))
                    {
                        wordToSolve.IsMatched = true;
                    }
                }
                else
                {
                    string pattern = WildCardToRegular(word);
                    wordToSolve.PossibleMatches = wordList
                        .Where(word => word.Length == wordToSolve.Length && Regex.IsMatch(word, pattern))
                        .Count();
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

        private async Task<bool> GetNextIteration()
        {
            Dictionary<int, string> clonedGivenValues = givenValues.ToDictionary(entry => entry.Key, entry => entry.Value);
            Dictionary<int, string> clonedPossibleValues = possibleValues.ToDictionary(entry => entry.Key, entry => entry.Value);

            bool canContinue = true;
            while (canContinue)
            {
                canContinue = false;
                foreach (int x in clonedPossibleValues.Keys.ToList())
                {
                    if (clonedPossibleValues[x].Length > 0)
                    {
                        string s = clonedPossibleValues[x].Substring(0, 1);
                        clonedGivenValues.Add(x, s);
                        clonedPossibleValues.Remove(x);
                        canContinue = true;
                        break;
                    }
                }
                if (canContinue)
                {
                    Solver solver = new Solver(clonedGivenValues, clonedPossibleValues, wordList, wordsToSolve);
                    bool result = await solver.Solve();
                    int unsolveableWords = wordsToSolve.Where(x => !x.IsMatched && x.PossibleMatches < 1).Count();

                    if (result || unsolveableWords == 0) canContinue = false;
                }
                else
                {
                    return false;
                }
            }
            return canContinue;
        }

        private static String WildCardToRegular(String value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }
    }
}
