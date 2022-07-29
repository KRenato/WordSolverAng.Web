using WordSolverAng.Api.Constants;

namespace WordSolverAng.Api.Models
{
    public class WordModel
    {
        public string? Value { get; init; }

        public LetterPattern[]? LetterPatterns { get; init; }
    }
}
