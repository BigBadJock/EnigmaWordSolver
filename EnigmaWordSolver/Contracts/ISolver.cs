namespace EnigmaWordSolver.Contracts
{
    public interface ISolver
    {
        Task<bool> Solve();
        Dictionary<int, string> GivenValues { get; }
    }
}
