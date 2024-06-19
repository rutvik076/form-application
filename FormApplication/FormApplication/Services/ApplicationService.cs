using FormApplication.DTOs;
using FormApplication.Models;
using FormApplication.Repositories;

namespace FormApplication.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task AddApplicationAsync(ApplicationDTO applicationDto)
        {
            var application = new Application
            {
                Id = Guid.NewGuid().ToString(),
                programId = applicationDto.programId,
                CandidateId = applicationDto.CandidateId,
                FirstName = applicationDto.FirstName,
                LastName = applicationDto.LastName,
                Email = applicationDto.Email,
                Phone = applicationDto.Phone,
                Nationality = applicationDto.Nationality,
                CurrentResidence = applicationDto.CurrentResidence,
                IDNumber = applicationDto.IDNumber,
                DateOfBirth = applicationDto.DateOfBirth,
                Gender = applicationDto.Gender,
            };
            await _applicationRepository.AddApplicationAsync(application);
        }

        public async Task<Application> GetApplicationAsync(string id, string programId)
        {
            return await _applicationRepository.GetApplicationAsync(id, programId);
        }
    }
}
