using EnigmaWordSolver.DTO;

namespace EnigmaWordSolver
{
    public interface IEnigmaWord
    {
        Dictionary<int, string> GivenValues { get; }

        Dictionary<int, string> PossibleValues { get; }

        List<WordToSolve> WordsToSolve { get; }

        bool AddWord(string codedWord);

        void AddGivenValue(int number, string value);

        Task<bool> Solve();
    }
}