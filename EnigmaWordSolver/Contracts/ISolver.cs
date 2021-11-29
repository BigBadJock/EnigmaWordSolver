namespace EnigmaWordSolver.Contracts
{
    public interface ISolver
    {
        Task<bool> Solve();
    }
}
