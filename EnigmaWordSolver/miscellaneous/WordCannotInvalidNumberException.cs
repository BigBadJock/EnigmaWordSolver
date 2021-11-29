namespace EnigmaWordSolver.miscellaneous
{
    public class WordCannotInvalidNumberException : Exception
    {
        public override string Message
        {
            get
            {
                return "Word cannot Invalid Number";
            }
        }
    }
}
