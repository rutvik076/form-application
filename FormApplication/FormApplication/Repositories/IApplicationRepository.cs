using FormApplication.Models;

namespace FormApplication.Repositories
{
    public interface IApplicationRepository
    {
        Task AddApplicationAsync(Application application);
        Task<Application> GetApplicationAsync(string id, string programId);
    }
}
