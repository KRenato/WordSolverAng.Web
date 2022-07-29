using WordSolverAng.Api.Core;

namespace WordSolverAng.Api.Services.Interfaces
{
    public interface ILetterOptionService
    {
        IDictionary<int, HashSet<char>> AvailableLetters { get; }
        HashSet<char> UnplacedLetters { get; }

        void SetPatterns(IEnumerable<Word> wordsTried);
    }
}