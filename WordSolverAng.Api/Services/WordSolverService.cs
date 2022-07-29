using System.Collections.Concurrent;
using WordSolverAng.Api.Constants;
using WordSolverAng.Api.Core;
using WordSolverAng.Api.Services.Interfaces;

namespace WordSolverAng.Api.Services
{
    public class WordSolverService : IWordSolverService
    {
        private readonly int _wordLength;
        private readonly ILetterOptionService _letterOptionService;
        private readonly IWordRepositoryService _wordRepo;

        public WordSolverService(IConfiguration config, IWordRepositoryService wordRepo, ILetterOptionService letterOptionService)
        {
            _wordLength = int.Parse(config[ConfigValues.WordLength]);
            _letterOptionService = letterOptionService;
            _wordRepo = wordRepo;
        }

        public string? GetBestWord(IEnumerable<Word>? wordsTried = null)
        {
            HashSet<string> matches = GetMatchingWords(wordsTried ?? Enumerable.Empty<Word>());

            var wordScores = new ConcurrentDictionary<string, int>();

            Parallel.ForEach(matches, wordToMatch =>
            {
                wordScores.TryAdd(wordToMatch, GetWordScore(matches, wordToMatch));
            });

            return GetHighestScoringWord(wordScores);
        }

        public async Task<string?> GetBestWordAsync(IEnumerable<Word>? wordsTried = null)
        {
            HashSet<string> matches = GetMatchingWords(wordsTried ?? Enumerable.Empty<Word>());

            var wordScores = new ConcurrentDictionary<string, int>();

            await Parallel.ForEachAsync(matches, async (wordToMatch, _) =>
            {
                wordScores.TryAdd(wordToMatch, await GetWordScoreAsync(matches, wordToMatch));
            });

            return GetHighestScoringWord(wordScores);
        }

        private HashSet<string> GetMatchingWords(IEnumerable<Word> wordsTried)
        {
            if (!ArePatternsValid(wordsTried))
                throw new ArgumentException("Entered patterns are invalid.", nameof(wordsTried));

            _letterOptionService.SetPatterns(wordsTried);

            var words = _wordRepo.GetWords();
            var matches = new HashSet<string>(words.Where(w => IsMatch(w, wordsTried)));
            return matches;
        }

        private int GetWordScore(HashSet<string> words, string wordToMatch)
        {
            int wordScore = 0;

            foreach (var word in words)
            {
                for (int i = 0; i < _wordLength; i++)
                {
                    if (word.Contains(wordToMatch[i]))
                    {
                        wordScore++;
                    }
                }
            }

            return wordScore;
        }

        private async ValueTask<int> GetWordScoreAsync(HashSet<string> words, string wordToMatch)
        {
            return await Task.Run(() => GetWordScore(words, wordToMatch));
        }

        private static string? GetHighestScoringWord(IDictionary<string, int> words)
        {
            return words
                .OrderByDescending(w => w.Key.Distinct().Count())
                .ThenByDescending(w => w.Value)
                .Select(w => w.Key)
                .FirstOrDefault();
        }

        private static bool ArePatternsValid(IEnumerable<Word> wordsTried) => wordsTried.All(w => w.ArePatternsValid());

        private bool IsMatch(string word, IEnumerable<Word> wordsTried)
        {
            return wordsTried.Any(wt => wt.ToString() == word) == false
                && AreLettersAvailable(word)
                && HasUnmatchedLetters(word);
        }

        private bool AreLettersAvailable(string word)
        {
            for (int i = 0; i < _wordLength; i++)
            {
                if (!_letterOptionService
                    .AvailableLetters[i]
                    .Any(l => l == word[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private bool HasUnmatchedLetters(string word)
        {
            if (_letterOptionService.UnplacedLetters.Count == 0)
            {
                return true;
            }

            return word.Intersect(_letterOptionService.UnplacedLetters).Count() == _letterOptionService.UnplacedLetters.Count;
        }
    }
}
