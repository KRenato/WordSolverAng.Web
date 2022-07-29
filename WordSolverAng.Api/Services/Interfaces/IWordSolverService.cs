using WordSolverAng.Api.Core;

namespace WordSolverAng.Api.Services.Interfaces
{
    public interface IWordSolverService
    {
        string? GetBestWord(IEnumerable<Word>? wordsTried = null);

        Task<string?> GetBestWordAsync(IEnumerable<Word>? wordsTried = null);
    }
}