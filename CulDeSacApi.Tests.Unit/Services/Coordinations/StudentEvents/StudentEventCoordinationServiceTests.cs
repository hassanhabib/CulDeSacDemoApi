using System;
using System.Linq.Expressions;
using CulDeSacApi.Models.LibraryAccounts;
using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.Coordinations.StudentEvents;
using CulDeSacApi.Services.Orchestrations.LibraryAccounts;
using CulDeSacApi.Services.Orchestrations.StudentEvents;
using Moq;
using Tynamix.ObjectFiller;

namespace CulDeSacApi.Tests.Unit.Services.Coordinations.StudentEvents
{
    public partial class StudentEventCoordinationServiceTests
    {
        private readonly Mock<IStudentEventOrchestrationService> studentEventOrchestrationServiceMock;
        private readonly Mock<ILibraryAccountOrchestrationService> libraryAccountOrchestrationServiceMock;
        private readonly IStudentEventCoordinationService studentEventCoordinationService;

        public StudentEventCoordinationServiceTests()
        {
            this.studentEventOrchestrationServiceMock = new Mock<IStudentEventOrchestrationService>();
            this.libraryAccountOrchestrationServiceMock = new Mock<ILibraryAccountOrchestrationService>();

            this.studentEventCoordinationService = new StudentEventCoordinationService(
                studentEventOrchestrationService: this.studentEventOrchestrationServiceMock.Object,
                libraryAccountOrchestrationService: this.libraryAccountOrchestrationServiceMock.Object);
        }

        private static Student CreateRandomStudent() =>
            CreateStudentFiller().Create();

        private static Expression<Func<LibraryAccount, bool>> SameLibraryAccountAs(
            LibraryAccount expectedLibraryAccount)
        {
            return actualLibraryAccount => 
                actualLibraryAccount.StudentId == expectedLibraryAccount.StudentId
                && actualLibraryAccount.Id != Guid.Empty;
        }

        private static Filler<Student> CreateStudentFiller() =>
            new Filler<Student>();
    }
}
