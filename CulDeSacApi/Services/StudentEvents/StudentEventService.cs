using System;
using System.Threading.Tasks;
using CulDeSacApi.Brokers.Queues;
using CulDeSacApi.Models.Students;

namespace CulDeSacApi.Services.StudentEvents
{
    public partial class StudentEventService : IStudentEventService
    {
        private readonly IQueueBroker queueBroker;

        public StudentEventService(IQueueBroker queueBroker) =>
            this.queueBroker = queueBroker;

        public void ListenToStudentEvent(Func<Student, ValueTask> studentEventHandler)
        {
            throw new NotImplementedException();
        }
    }
}
