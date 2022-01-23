using System;
using System.Threading;
using System.Threading.Tasks;
using CulDeSacApi.Models.Students;
using Microsoft.Azure.ServiceBus;
using Moq;
using Xunit;

namespace CulDeSacApi.Tests.Unit.Services.StudentEvents
{
    public partial class StudentEventServiceTests
    {
        [Fact]
        public void ShouldListenToStudentEvent()
        {
            // given
            var studentEventHandlerMock =
                new Mock<Func<Student, ValueTask>>();

            Student randomStudent = CreateRandomStudent();
            Student incomingStudent = randomStudent;

            Message studentMessage =
                CreateStudentMessage(incomingStudent);

            this.queueBrokerMock.Setup(broker =>
                broker.ListenToStudentsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()))
                        .Callback<Func<Message, CancellationToken, Task>>(eventFunction =>
                            eventFunction.Invoke(studentMessage, It.IsAny<CancellationToken>()));

            // when
            studentEventService.ListenToStudentEvent(
                studentEventHandler: studentEventHandlerMock.Object);

            // then
            studentEventHandlerMock.Verify(handler =>
                handler.Invoke(incomingStudent),
                    Times.Once());

            this.queueBrokerMock.Verify(broker =>
                broker.ListenToStudentsQueue(
                    It.IsAny<Func<Message, CancellationToken, Task>>()),
                        Times.Once());

            this.queueBrokerMock.VerifyNoOtherCalls();
        }
    }
}
