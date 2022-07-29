using WordSolverAng.Api.Constants;
using WordSolverAng.Api.Services.Interfaces;

namespace WordSolverAng.Api.Services
{
    public class WordRepositoryService : IWordRepositoryService
    {
        private readonly IEnumerable<string> _cachedWords;

        public WordRepositoryService(IConfiguration config, IWebHostEnvironment _environment)
        {
            var wordLength = int.Parse(config[ConfigValues.WordLength]);
            var wordFilePath = config[ConfigValues.WordFilePath];

            var words = File.ReadAllLines(wordFilePath);

            _cachedWords = words
                .Where(w => w.Length == wordLength)
                .Select(w => w.ToLower())
                .Distinct();
        }

        public IEnumerable<string> GetWords()
        {
            return _cachedWords;
        }
    }
}
