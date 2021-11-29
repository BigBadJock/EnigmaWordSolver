namespace EnigmaWordSolver.miscellaneous
{
    public class GivenValueInvalidException : Exception
    {
        public override string Message
        {
            get
            {
                return "Given Values Invalid";
            }
        }
    }
}
