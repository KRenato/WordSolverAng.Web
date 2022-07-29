namespace WordSolverAng.Api.Services.Interfaces
{
    public interface IWordRepositoryService
    {
        IEnumerable<string> GetWords();
    }
}