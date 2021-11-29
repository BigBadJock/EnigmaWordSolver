namespace EnigmaWordSolver.miscellaneous
{
    public class NoWordsException : Exception
    {
        public override string Message
        {
            get
            {
                return "No words to solve";
            }
        }
    }
}
