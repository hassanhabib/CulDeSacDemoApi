using System;
using System.Text;
using System.Threading.Tasks;
using CulDeSacApi.Brokers.Queues;
using CulDeSacApi.Models.Students;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace CulDeSacApi.Services.StudentEvents
{
    public partial class StudentEventService : IStudentEventService
    {
        private readonly IQueueBroker queueBroker;

        public StudentEventService(IQueueBroker queueBroker) =>
            this.queueBroker = queueBroker;

        public void ListenToStudentEvent(Func<Student, ValueTask> studentEventHandler)
        {
            this.queueBroker.ListenToStudentsQueue(async (message, token) =>
            {
                Student incomingStudent = MapToStudent(message);
                await studentEventHandler(incomingStudent);
            });
        }

        private static Student MapToStudent(Message message)
        {
            string serializedStudent =
                Encoding.UTF8.GetString(message.Body);

            return JsonConvert.DeserializeObject<Student>(serializedStudent);
        }
    }
}
