using System;
using System.Linq.Expressions;
using System.Text;
using CulDeSacApi.Brokers.Queues;
using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.StudentEvents;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Azure.ServiceBus;
using Moq;
using Newtonsoft.Json;
using Tynamix.ObjectFiller;

namespace CulDeSacApi.Tests.Unit.Services.StudentEvents
{
    public partial class StudentEventServiceTests
    {
        private readonly Mock<IQueueBroker> queueBrokerMock;
        private readonly IStudentEventService studentEventService;
        private readonly ICompareLogic comparelogic;

        public StudentEventServiceTests()
        {
            this.queueBrokerMock = new Mock<IQueueBroker>();
            this.comparelogic = new CompareLogic();

            this.studentEventService = new StudentEventService(
                queueBroker: this.queueBrokerMock.Object);
        }

        private static Message CreateStudentMessage(Student student)
        {
            string serializedStudent = JsonConvert.SerializeObject(student);
            byte[] studentBody = Encoding.UTF8.GetBytes(serializedStudent);

            return new Message
            {
                Body = studentBody
            };
        }

        private Expression<Func<Student, bool>> SameStudentAs(Student expectedStudent)
        {
            return actualStudent =>
                this.comparelogic.Compare(expectedStudent, actualStudent).AreEqual;
        }

        private static Student CreateRandomStudent() =>
            CreateStudentFiller().Create();

        private static Filler<Student> CreateStudentFiller() =>
            new Filler<Student>();
    }
}
