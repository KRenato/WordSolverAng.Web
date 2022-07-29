using WordSolverAng.Api.Constants;
using WordSolverAng.Api.Core;
using WordSolverAng.Api.Services.Interfaces;

namespace WordSolverAng.Api.Services
{
    public class LetterOptionService : ILetterOptionService
    {
        private readonly int _wordLength;
        private readonly Dictionary<LetterPattern, Action<int, Word>> _actionMap;
        private readonly Dictionary<int, HashSet<char>> _availableLetters = new();

        public LetterOptionService(IConfiguration config)
        {
            _wordLength = int.Parse(config[ConfigValues.WordLength]);
            _actionMap = new() {
                { LetterPattern.DoesNotMatch, EliminateLetter },
                { LetterPattern.WordMatch, EliminateLetterForPlace },
                { LetterPattern.ExactMatch, LetterIsAHit }
            };

            InitializeProperties();
        }

        public IDictionary<int, HashSet<char>> AvailableLetters => _availableLetters;

        public HashSet<char> UnplacedLetters { get; private set; } = new();

        public void SetPatterns(IEnumerable<Word> wordsTried)
        {
            InitializeProperties();

            foreach (var word in wordsTried)
            {
                for (int i = 0; i < _wordLength; i++)
                {
                    _actionMap[word.GetPatternAt(i)].Invoke(i, word);
                }
            }
        }

        private void InitializeProperties()
        {
            for (int i = 0; i < _wordLength; i++)
            {
                _availableLetters.TryAdd(i, new HashSet<char>());
                _availableLetters[i].Clear();

                for (char c = 'a'; c <= 'z'; c++)
                {
                    _availableLetters[i].Add(c);
                }
            }
        }

        private void EliminateLetter(int place, Word word)
        {
            var letter = word.GetCharacterAt(place);

            for (int i = 0; i < _wordLength; i++)
            {
                _availableLetters[i].Remove(letter);
            }
        }

        private void EliminateLetterForPlace(int place, Word word)
        {
            var letter = word.GetCharacterAt(place);

            _availableLetters[place].Remove(letter);
            if (!UnplacedLetters.Any(l => l == letter))
            {
                UnplacedLetters.Add(letter);
            }
        }

        private void LetterIsAHit(int place, Word word)
        {
            var letter = word.GetCharacterAt(place);

            _availableLetters[place].RemoveWhere(l => l != letter);
            UnplacedLetters.Remove(letter);
        }
    }
}
