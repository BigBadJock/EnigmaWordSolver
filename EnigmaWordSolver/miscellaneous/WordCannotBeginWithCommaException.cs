namespace EnigmaWordSolver.miscellaneous
{
    public class WordCannotBeginWithCommaException : Exception
    {
        public override string Message
        {
            get
            {
                return "Word cannot begin with comma";
            }
        }
    }
}
