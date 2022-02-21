using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.Foundations.StudentEvents;
using CulDeSacApi.Services.Foundations.Students;

namespace CulDeSacApi.Services.Orchestrations.StudentEvents
{
    public class StudentEventOrchestrationService : IStudentEventOrchestrationService
    {
        private readonly IStudentEventService studentEventService;
        private readonly IStudentService studentService;

        public StudentEventOrchestrationService(
            IStudentEventService studentEventService,
            IStudentService studentService)
        {
            this.studentEventService = studentEventService;
            this.studentService = studentService;
        }

        public void ListenToStudentEvents(Func<Student, ValueTask> studentEventHandler)
        {
            this.studentEventService.ListenToStudentEvent(async (student) =>
            {
                await this.studentService.AddStudentAsync(student);
            });
        }
    }
}
