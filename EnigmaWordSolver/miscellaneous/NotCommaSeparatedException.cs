namespace EnigmaWordSolver.miscellaneous
{
    public class NotCommaSeparatedException : Exception
    {
        public override string Message
        {
            get
            {
                return "Word must be comma seperated";
            }
        }
    }
}
