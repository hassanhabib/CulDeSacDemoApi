using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.Students;

namespace CulDeSacApi.Services.Foundations.LocalStudentEvents
{
    public interface ILocalStudentEventService
    {
        void ListenToStudentEvent(Func<Student, ValueTask<Student>> studentEventHandler);
        ValueTask PublishStudentAsync(Student student);
    }
}
