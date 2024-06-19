using FormApplication.DTOs;
using FormApplication.Models;
using FormApplication.Repositories;

namespace FormApplication.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task AddQuestionAsync(CreateQuestionDto questionDto, string programId)
        {
            Question question = questionDto.Type switch
            {
                "Paragraph" => new ParagraphQuestion { Id = Guid.NewGuid().ToString(), programId = programId, QuestionText = questionDto.QuestionText },
                "YesNo" => new YesNoQuestion { Id = Guid.NewGuid().ToString(), programId = programId, QuestionText = questionDto.QuestionText },
                "Dropdown" => new DropdownQuestion { Id = Guid.NewGuid().ToString(), programId = programId, QuestionText = questionDto.QuestionText, Options = questionDto.Options },
                "MultipleChoice" => new MultipleChoiceQuestion { Id = Guid.NewGuid().ToString(), programId = programId, QuestionText = questionDto.QuestionText, Options = questionDto.Options },
                "Date" => new DateQuestion { Id = Guid.NewGuid().ToString(), programId = programId, QuestionText = questionDto.QuestionText },
                "Number" => new NumberQuestion { Id = Guid.NewGuid().ToString(), programId = programId, QuestionText = questionDto.QuestionText },
                _ => throw new ArgumentException("Invalid question type")
            };

            await _questionRepository.AddQuestionAsync(question);
        }

        public async Task UpdateQuestionAsync(string id, UpdateQuestionDto questionDto, string programId)
        {
            var question = await _questionRepository.GetQuestionAsync(id, programId);

            if (question != null)
            {
                question.QuestionText = questionDto.QuestionText;
                question.Type = questionDto.Type;

                // Check if the question type requires options
                if (question is DropdownQuestion || question is MultipleChoiceQuestion)
                {
                    if (questionDto.Options != null)
                    {
                        // Update options based on the question type
                        if (question is DropdownQuestion dropdownQuestion)
                        {
                            dropdownQuestion.Options = questionDto.Options;
                        }
                        else if (question is MultipleChoiceQuestion multipleChoiceQuestion)
                        {
                            multipleChoiceQuestion.Options = questionDto.Options;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Options are required for Dropdown and MultipleChoice question types.");
                    }
                }

                await _questionRepository.UpdateQuestionAsync(question);
            }
        }


        public async Task<Question> GetQuestionAsync(string id, string programId)
        {
            return await _questionRepository.GetQuestionAsync(id, programId);
        }

        public async Task<IEnumerable<Question>> GetQuestionsByProgramIdAsync(string programId)
        {
            return await _questionRepository.GetQuestionsByProgramIdAsync(programId);
        }

        public async Task DeleteQuestionAsync(string id, string programId)
        {
            await _questionRepository.DeleteQuestionAsync(id, programId);
        }
    }

}
