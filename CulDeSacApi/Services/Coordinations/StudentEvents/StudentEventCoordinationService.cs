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
            throw new System.NotImplementedException();
        }
    }
}
