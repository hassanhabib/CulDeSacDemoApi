using System;
using System.Threading.Tasks;
using CulDeSacApi.Brokers.Events;
using CulDeSacApi.Brokers.Loggings;
using CulDeSacApi.Models.Students;

namespace CulDeSacApi.Services.Foundations.LocalStudentEvents
{
    public partial class LocalStudentEventService : ILocalStudentEventService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IEventBroker eventBroker;

        public LocalStudentEventService(ILoggingBroker loggingBroker, IEventBroker eventBroker)
        {
            this.loggingBroker = loggingBroker;
            this.eventBroker = eventBroker;
        }

        public void ListenToStudentEvent(Func<Student, ValueTask<Student>> studentEventHandler) =>
            this.eventBroker.ListenToStudentEvent(studentEventHandler);

        public async ValueTask PublishStudentAsync(Student student) =>
            await Trace(
                function: async () => { await this.eventBroker.PublishStudentEventAsync(student); },
                activityName: $"CulDeSacDemoApi.LocalStudentEventService.PublishStudentAsync");
    }
}
