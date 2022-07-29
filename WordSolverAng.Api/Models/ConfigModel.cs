namespace WordSolverAng.Api.Models
{
    public class ConfigModel
    {
        public int NumberOfGuesses { get; init; }

        public int WordLength { get; init; }

        public string WordFilePath { get; init; } = string.Empty;

        public string? InitialWord { get; init; }
    }
}
