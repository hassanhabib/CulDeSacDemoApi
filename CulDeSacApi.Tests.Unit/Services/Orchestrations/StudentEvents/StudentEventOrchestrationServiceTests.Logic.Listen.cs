﻿using System;
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

            this.studentEventServiceMock.Setup(service =>
                service.ListenToStudentEvent(It.IsAny<Func<Student, ValueTask>>()))
                    .Callback<Func<Student, ValueTask>>(eventFunction =>
                        eventFunction.Invoke(incomingStudent));

            // when
            this.studentEventOrchestrationService.ListenToStudentEvents();

            // then
            this.studentEventServiceMock.Verify(service =>
                service.ListenToStudentEvent(It.IsAny<Func<Student, ValueTask>>()),
                    Times.Once);

            this.studentServiceMock.Verify(service =>
                service.AddStudentAsync(incomingStudent),
                    Times.Once);

            this.studentEventServiceMock.VerifyNoOtherCalls();
            this.studentServiceMock.VerifyNoOtherCalls();
        }
    }
}