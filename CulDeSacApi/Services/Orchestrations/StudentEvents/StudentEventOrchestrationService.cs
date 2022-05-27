using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.Foundations.LocalStudentEvents;
using CulDeSacApi.Services.Foundations.StudentEvents;
using CulDeSacApi.Services.Foundations.Students;

namespace CulDeSacApi.Services.Orchestrations.StudentEvents
{
    public class StudentEventOrchestrationService : IStudentEventOrchestrationService
    {
        private readonly IStudentEventService studentEventService;
        private readonly IStudentService studentService;
        private readonly ILocalStudentEventService localStudentEventService;

        public StudentEventOrchestrationService(
            IStudentEventService studentEventService,
            IStudentService studentService,
            ILocalStudentEventService localStudentEventService)
        {
            this.studentEventService = studentEventService;
            this.studentService = studentService;
            this.localStudentEventService = localStudentEventService;
        }

        public void ListenToStudentEvents()
        {
            this.studentEventService.ListenToStudentEvent(async (student) =>
            {
                await this.studentService.AddStudentAsync(student);
                await this.localStudentEventService.PublishStudentAsync(student);
            });
        }
    }
}
