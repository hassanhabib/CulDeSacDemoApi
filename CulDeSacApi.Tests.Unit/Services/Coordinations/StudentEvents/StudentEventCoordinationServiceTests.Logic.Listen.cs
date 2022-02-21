using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryAccounts;
using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.Coordinations.StudentEvents;
using CulDeSacApi.Services.Orchestrations.LibraryAccounts;
using CulDeSacApi.Services.Orchestrations.StudentEvents;
using Moq;
using Tynamix.ObjectFiller;
using Xunit;

namespace CulDeSacApi.Tests.Unit.Services.Coordinations.StudentEvents
{
    public partial class StudentEventCoordinationServiceTests
    {
        [Fact]
        public void ShouldListenToStudentEvents()
        {
            // given
            Student randomStudent = CreateRandomStudent();
            Student incomingStudent = randomStudent;
            
            var expectedInputLibraryAccount = new LibraryAccount
            {
                Id = Guid.NewGuid(),
                StudentId = incomingStudent.Id
            };

            this.studentEventOrchestrationServiceMock.Setup(service =>
                service.ListenToStudentEvents(It.IsAny<Func<Student, ValueTask>>()))
                    .Callback<Func<Student, ValueTask>>(eventHandler =>
                        eventHandler.Invoke(incomingStudent));

            // when
            this.studentEventCoordinationService.ListenToStudentEvents();

            // then
            this.studentEventOrchestrationServiceMock.Verify(service =>
                service.ListenToStudentEvents(It.IsAny<Func<Student, ValueTask>>()),
                    Times.Once);

            this.libraryAccountOrchestrationServiceMock.Verify(service =>
                service.CreateLibraryAccountAsync(It.Is(SameLibraryAccountAs(
                    expectedInputLibraryAccount))),
                        Times.Once);

            this.studentEventOrchestrationServiceMock.VerifyNoOtherCalls();
            this.libraryAccountOrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
