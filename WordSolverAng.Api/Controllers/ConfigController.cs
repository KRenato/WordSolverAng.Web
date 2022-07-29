using Microsoft.AspNetCore.Mvc;
using WordSolverAng.Api.Models;
using WordSolverAng.Api.Services.Interfaces;

namespace WordSolverAng.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : Controller
    {
        private readonly IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpGet]
        public ConfigModel Get()
        {
            return _configService.Config;
        }
    }
}
