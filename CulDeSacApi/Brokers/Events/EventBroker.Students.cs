using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.Students;

namespace CulDeSacApi.Brokers.Events
{
    public partial class EventBroker
    {
        private static Func<Student, ValueTask<Student>> StudentEventHandler;

        public void ListenToStudentEvent(Func<Student, ValueTask<Student>> studentEventHandler) =>
            StudentEventHandler = studentEventHandler;

        public async ValueTask PublishStudentEventAsync(Student student) =>
            await StudentEventHandler(student);
    }
}
