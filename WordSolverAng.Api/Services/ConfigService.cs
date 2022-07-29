using WordSolverAng.Api.Constants;
using WordSolverAng.Api.Models;
using WordSolverAng.Api.Services.Interfaces;

namespace WordSolverAng.Api.Services
{
    public class ConfigService : IConfigService
    {
        public ConfigService(IConfiguration config, IWordSolverService wordSolverService)
        {
            if (int.TryParse(config[ConfigValues.NumberOfGuesses], out int numberOfGuesses))
                throw new FormatException($"Unable to parse {ConfigValues.NumberOfGuesses} value from configuration.");
            if (int.TryParse(config[ConfigValues.WordLength], out int wordLength))
                throw new FormatException($"Unable to parse {ConfigValues.WordLength} value from configuration.");
            if (string.IsNullOrEmpty(config[ConfigValues.WordFilePath]))
                throw new FormatException($"Unable to parse {ConfigValues.WordFilePath} value from configuration.");

            Config = new ConfigModel
            {
                NumberOfGuesses = numberOfGuesses,
                WordLength = wordLength,
                WordFilePath = config[ConfigValues.WordFilePath],
                InitialWord = wordSolverService.GetBestWord()
            };
        }

        public ConfigModel Config { get; }
    }
}
