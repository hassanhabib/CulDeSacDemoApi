using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.Students;

namespace CulDeSacApi.Brokers.Events
{
    public partial interface IEventBroker
    {
        void ListenToStudentEvent(Func<Student, ValueTask<Student>> studentEventHandler);
        ValueTask PublishStudentEventAsync(Student student);
    }
}
