using CulDeSacApi.Brokers.Events;
using CulDeSacApi.Services.Foundations.LocalStudentEvents;
using Moq;

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
    }
}
