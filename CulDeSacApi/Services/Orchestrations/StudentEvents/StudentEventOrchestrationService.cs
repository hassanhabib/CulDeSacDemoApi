using System.Threading.Tasks;
using CulDeSacApi.Brokers.Loggings;
using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.Foundations.LocalStudentEvents;
using CulDeSacApi.Services.Foundations.StudentEvents;
using CulDeSacApi.Services.Foundations.Students;

namespace CulDeSacApi.Services.Orchestrations.StudentEvents
{
    public partial class StudentEventOrchestrationService : IStudentEventOrchestrationService
    {
        private readonly IStudentEventService studentEventService;
        private readonly IStudentService studentService;
        private readonly ILocalStudentEventService localStudentEventService;
        private readonly ILoggingBroker loggingBroker;

        public StudentEventOrchestrationService(
            IStudentEventService studentEventService,
            IStudentService studentService,
            ILocalStudentEventService localStudentEventService,
            ILoggingBroker loggingBroker)
        {
            this.studentEventService = studentEventService;
            this.studentService = studentService;
            this.localStudentEventService = localStudentEventService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Student> AddStudentAsync(Student student) =>
            await Trace(
                function: async () =>
                    {
                        Student newStudent = await this.studentService.AddStudentAsync(student);
                        await this.localStudentEventService.PublishStudentAsync(student);

                        return newStudent;
                    },
                activityName: $"CulDeSacDemoApi.StudentEventOrchestrationService.AddStudentAsync");

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
