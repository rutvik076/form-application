using FormApplication.DTOs;
using FormApplication.Models;

namespace FormApplication.Services
{
    public interface IApplicationService
    {
        Task AddApplicationAsync(ApplicationDTO applicationDto);
        Task<Application> GetApplicationAsync(string id, string programId);
    }
}
