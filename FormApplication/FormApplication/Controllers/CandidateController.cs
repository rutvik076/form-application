using FormApplication.DTOs;
using FormApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IQuestionService _questionService;

        public CandidateController(IApplicationService applicationService, IQuestionService questionService)
        {
            _applicationService = applicationService;
            _questionService = questionService;
        }

        [HttpPost("applications")]
        public async Task<IActionResult> AddApplication([FromBody] ApplicationDTO applicationDto)
        {
            await _applicationService.AddApplicationAsync(applicationDto);
            return Ok();
        }

        [HttpGet("applications/{id}")]
        public async Task<IActionResult> GetApplication(string id, [FromQuery] string programId)
        {
            var application = await _applicationService.GetApplicationAsync(id, programId);

            if (application == null)
                return NotFound();

            // Fetch questions based on programId
            application.Questions = (await _questionService.GetQuestionsByProgramIdAsync(programId)).ToList();

            return Ok(application);
        }
    }
}
