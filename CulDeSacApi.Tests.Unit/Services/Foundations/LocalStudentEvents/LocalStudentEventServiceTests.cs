using CulDeSacApi.Brokers.Events;
using CulDeSacApi.Brokers.Loggings;
using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.Foundations.LocalStudentEvents;
using Moq;
using Tynamix.ObjectFiller;

namespace CulDeSacApi.Tests.Unit.Services.Foundations.LocalStudentEvents
{

    public partial class LocalStudentEventServiceTests
    {
        private readonly Mock<IEventBroker> eventBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILocalStudentEventService localStudentEventService;

        public LocalStudentEventServiceTests()
        {
            this.eventBrokerMock = new Mock<IEventBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.localStudentEventService = new LocalStudentEventService(
                eventBroker: this.eventBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Student CreateRandomStudent() =>
            CreateStudentFiller().Create();

        private static Filler<Student> CreateStudentFiller() =>
            new Filler<Student>();
    }
}
