using CulDeSacApi.Models.Students;
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
        private readonly IStudentEventOrchestrationService studentEventOrchestrationService;

        public StudentEventOrchestrationServiceTests()
        {
            this.studentEventServiceMock = new Mock<IStudentEventService>();
            this.studentServiceMock = new Mock<IStudentService>(MockBehavior.Strict);

            this.studentEventOrchestrationService = new StudentEventOrchestrationService(
                studentEventService: this.studentEventServiceMock.Object,
                studentService: this.studentServiceMock.Object);
        }

        private static Student CreateRandomStudent() =>
          CreateStudentFiller().Create();

        private static Filler<Student> CreateStudentFiller() =>
            new Filler<Student>();
    }
}
