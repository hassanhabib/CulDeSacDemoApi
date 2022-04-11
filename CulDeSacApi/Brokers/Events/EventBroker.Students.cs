using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.Students;

namespace CulDeSacApi.Brokers.Events
{
    public partial class EventBroker
    {
        private Func<Student, ValueTask<Student>> studentEventHandler;

        public void ListenToStudentEvent(Func<Student, ValueTask<Student>> studentEventHandler) =>
            this.studentEventHandler = studentEventHandler;

        public async ValueTask PublishStudentEventAsync(Student student) =>
            await this.studentEventHandler(student);
    }
}
