using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.Foundations.LocalStudentEvents;
using CulDeSacApi.Services.Foundations.StudentEvents;
using CulDeSacApi.Services.Foundations.Students;
using CulDeSacApi.Services.Orchestrations.StudentEvents;
using Moq;
using Tynamix.ObjectFiller;

namespace CulDeSacApi.Tests.Unit.Services.Orchestrations.StudentEvents
{
    public partial class StudentEventOrchestrationServiceTests
    {
        private readonly Mock<IStudentEventService> studentEventServiceMock;
        private readonly Mock<IStudentService> studentServiceMock;
        private readonly Mock<ILocalStudentEventService> localStudentEventService;
        private readonly IStudentEventOrchestrationService studentEventOrchestrationService;

        public StudentEventOrchestrationServiceTests()
        {
            this.studentEventServiceMock = new Mock<IStudentEventService>();
            this.studentServiceMock = new Mock<IStudentService>(MockBehavior.Strict);
            this.localStudentEventService = new Mock<ILocalStudentEventService>(MockBehavior.Strict);

            this.studentEventOrchestrationService = new StudentEventOrchestrationService(
                studentEventService: this.studentEventServiceMock.Object,
                studentService: this.studentServiceMock.Object,
                localStudentEventService: this.localStudentEventService.Object);
        }

        private static Student CreateRandomStudent() =>
          CreateStudentFiller().Create();

        private static Filler<Student> CreateStudentFiller() =>
            new Filler<Student>();
    }
}
