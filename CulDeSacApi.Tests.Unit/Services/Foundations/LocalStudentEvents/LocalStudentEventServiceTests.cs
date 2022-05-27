using CulDeSacApi.Brokers.Events;
using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.Foundations.LocalStudentEvents;
using Moq;
using Tynamix.ObjectFiller;

namespace CulDeSacApi.Tests.Unit.Services.Foundations.LocalStudentEvents
{

    public partial class LocalStudentEventServiceTests
    {
        private readonly Mock<IEventBroker> eventBrokerMock;
        private readonly ILocalStudentEventService localStudentEventService;

        public LocalStudentEventServiceTests()
        {
            this.eventBrokerMock = new Mock<IEventBroker>();

            this.localStudentEventService = new LocalStudentEventService(
                eventBroker: this.eventBrokerMock.Object);
        }

        private static Student CreateRandomStudent() =>
            CreateStudentFiller().Create();

        private static Filler<Student> CreateStudentFiller() =>
            new Filler<Student>();
    }
}
