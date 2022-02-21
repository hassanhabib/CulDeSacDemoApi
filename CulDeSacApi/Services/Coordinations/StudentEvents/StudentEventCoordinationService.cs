using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryAccounts;
using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.Orchestrations.LibraryAccounts;
using CulDeSacApi.Services.Orchestrations.StudentEvents;

namespace CulDeSacApi.Services.Coordinations.StudentEvents
{
    public class StudentEventCoordinationService : IStudentEventCoordinationService
    {
        private readonly IStudentEventOrchestrationService studentEventOrchestrationService;
        private readonly ILibraryAccountOrchestrationService libraryAccountOrchestrationService;

        public StudentEventCoordinationService(
            IStudentEventOrchestrationService studentEventOrchestrationService,
            ILibraryAccountOrchestrationService libraryAccountOrchestrationService)
        {
            this.studentEventOrchestrationService = studentEventOrchestrationService;
            this.libraryAccountOrchestrationService = libraryAccountOrchestrationService;
        }

        public void ListenToStudentEvents()
        {
            this.studentEventOrchestrationService.ListenToStudentEvents(async (student) => 
                await AddStudentLibraryAccount(student));
        }

        private async Task AddStudentLibraryAccount(Student student)
        {
            var libraryAccount = new LibraryAccount
            {
                Id = Guid.NewGuid(),
                StudentId = student.Id
            };

            await this.libraryAccountOrchestrationService
                .CreateLibraryAccountAsync(libraryAccount);
        }
    }
}
