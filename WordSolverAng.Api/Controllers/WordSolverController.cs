using Microsoft.AspNetCore.Mvc;
using WordSolverAng.Api.Core;
using WordSolverAng.Api.Models;
using WordSolverAng.Api.Services.Interfaces;

namespace WordSolverAng.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordSolverController : Controller
    {
        private readonly ILogger<WordSolverController> _logger;
        private readonly IWordSolverService _wordSolverService;

        public WordSolverController(ILogger<WordSolverController> logger, IWordSolverService wordSolver)
        {
            _logger = logger;
            _wordSolverService = wordSolver;
        }

        [HttpPost]
        public async Task<string?> Post(WordModel[] words)
        {
            try
            {
                var convertedWords = words.Select(w => new Word(w));

                var bestWord = await _wordSolverService.GetBestWordAsync(convertedWords);

                return bestWord;
                //return _wordSolverService.GetBestWordAsync(convertedWords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in controller.");
                throw;
            }
        }
    }
}
