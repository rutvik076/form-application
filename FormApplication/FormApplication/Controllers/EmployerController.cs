using FormApplication.DTOs;
using FormApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployerController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public EmployerController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("programs/{programId}/questions")]
        public async Task<IActionResult> AddQuestion(string programId, [FromBody] CreateQuestionDto questionDto)
        {
            try
            {
                await _questionService.AddQuestionAsync(questionDto, programId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("programs/{programId}/questions/{id}")]
        public async Task<IActionResult> UpdateQuestion(string programId, string id, [FromBody] UpdateQuestionDto questionDto)
        {
            await _questionService.UpdateQuestionAsync(id, questionDto, programId);
            return Ok();
        }

        [HttpGet("programs/{programId}/questions")]
        public async Task<IActionResult> GetQuestions(string programId)
        {
            var questions = await _questionService.GetQuestionsByProgramIdAsync(programId);
            return Ok(questions);
        }

        [HttpDelete("programs/{programId}/questions/{id}")]
        public async Task<IActionResult> DeleteQuestion(string programId, string id)
        {
            try
            {
                await _questionService.DeleteQuestionAsync(id, programId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
