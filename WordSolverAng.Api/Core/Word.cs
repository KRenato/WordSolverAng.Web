using WordSolverAng.Api.Constants;
using WordSolverAng.Api.Models;

namespace WordSolverAng.Api.Core
{
    public class Word
    {
        private readonly string _word;
        private readonly LetterPattern[] _letterPatterns;

        public Word(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException(nameof(word));

            _word = word;
            int length = _word.Length;

            _letterPatterns ??= new LetterPattern[length];
            for (int i = 0; i < length; i++)
            {
                _letterPatterns[i] = LetterPattern.DoesNotMatch;
            }
        }

        public Word(WordModel word) : this(word?.Value ?? string.Empty)
        {
            if (word is null)
                throw new ArgumentNullException(nameof(word));
            if (word.Value is null)
                throw new ArgumentException($"Property {nameof(word)}.{nameof(word.Value)} must not be null.");
            if (word.LetterPatterns is null)
                throw new ArgumentException($"Property {nameof(word)}.{nameof(word.LetterPatterns)} must not be null.");

            _word = word.Value;
            _letterPatterns = word.LetterPatterns;
        }

        public bool ArePatternsValid() => !_letterPatterns.Any(p => p == LetterPattern.Unknown);

        public override string ToString() => _word;

        public LetterPattern GetPatternAt(int index)
        {
            if (index >= _word.Length)
                throw new IndexOutOfRangeException();

            return _letterPatterns[index];
        }

        public char GetCharacterAt(int index)
        {
            return _word[index];
        }

        public WordModel ToModel()
        {
            return new WordModel
            {
                Value = _word,
                LetterPatterns = _letterPatterns
            };
        }
    }
}
