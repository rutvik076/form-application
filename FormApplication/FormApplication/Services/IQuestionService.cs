using FormApplication.DTOs;
using FormApplication.Models;

namespace FormApplication.Services
{
    public interface IQuestionService
    {
        Task AddQuestionAsync(CreateQuestionDto questionDto, string programId);
        Task UpdateQuestionAsync(string id, UpdateQuestionDto questionDto, string programId);
        Task<Question> GetQuestionAsync(string id, string programId);
        Task<IEnumerable<Question>> GetQuestionsByProgramIdAsync(string programId);
        Task DeleteQuestionAsync(string id, string programId);
    }
}
