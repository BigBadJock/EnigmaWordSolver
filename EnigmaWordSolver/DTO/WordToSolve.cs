namespace EnigmaWordSolver.DTO
{
    public class WordToSolve
    {
        public WordToSolve(string code)
        {
            CodedWord = code;
            PossibleMatches = -1;
            string[] letters = CodedWord.Split(",");
            this.Length = letters.Length;
        }

        public string CodedWord { get; set; }

        public string UncodedWord { get; set; }
        public int PossibleMatches { get; set; }

        public string PossibleWord { get; set; }

        public int Length { get; }

        public bool IsMatched { get; set; }

    }
}
