using FormApplication.Models;

namespace FormApplication.Repositories
{
    public interface IQuestionRepository
    {
        Task AddQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task<Question> GetQuestionAsync(string id, string programId);
        Task<IEnumerable<Question>> GetQuestionsByProgramIdAsync(string programId);
        Task DeleteQuestionAsync(string id, string programId);
    }
}
