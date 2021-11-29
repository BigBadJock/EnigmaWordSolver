namespace EnigmaWordSolver.miscellaneous
{
    public class WordCannotHaveDoubleCommasException : Exception
    {
        public override string Message
        {
            get
            {
                return "Word cannot have double commas";
            }
        }
    }
}
