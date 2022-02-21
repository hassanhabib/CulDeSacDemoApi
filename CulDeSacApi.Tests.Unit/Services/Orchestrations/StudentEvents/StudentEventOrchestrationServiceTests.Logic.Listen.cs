using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.Students;
using Moq;
using Xunit;

namespace CulDeSacApi.Tests.Unit.Services.Orchestrations.StudentEvents
{
    public partial class StudentEventOrchestrationServiceTests
    {
        [Fact]
        public void ShouldListenAndAddStudent()
        {
            // given
            Student randomStudent = CreateRandomStudent();
            Student incomingStudent = randomStudent;
            
            var studentEventHandlerMock = 
                new Mock<Func<Student, ValueTask>>(MockBehavior.Strict);
            
            var mockSequence = new MockSequence();

            this.studentEventServiceMock.InSequence(mockSequence).Setup(service =>
                service.ListenToStudentEvent(It.IsAny<Func<Student, ValueTask>>()))
                    .Callback<Func<Student, ValueTask>>(eventFunction =>
                        eventFunction.Invoke(incomingStudent));

            this.studentServiceMock.InSequence(mockSequence).Setup(service =>
                service.AddStudentAsync(incomingStudent))
                    .ReturnsAsync(incomingStudent);

            studentEventHandlerMock.InSequence(mockSequence).Setup(handler =>
                handler(incomingStudent));

            // when
            this.studentEventOrchestrationService.ListenToStudentEvents(
                studentEventHandler: studentEventHandlerMock.Object);

            // then
            this.studentEventServiceMock.Verify(service =>
                service.ListenToStudentEvent(It.IsAny<Func<Student, ValueTask>>()),
                    Times.Once);

            this.studentServiceMock.Verify(service =>
                service.AddStudentAsync(incomingStudent),
                    Times.Once);

            studentEventHandlerMock.Verify(handler =>
                handler(incomingStudent),
                    Times.Once);

            this.studentEventServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }
    }
}
