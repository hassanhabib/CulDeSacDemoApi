using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CulDeSacApi.Brokers.Loggings;
using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.Orchestrations.StudentEvents;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace CulDeSacApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class StudentsController : RESTFulController
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IStudentEventOrchestrationService studentEventOrchestrationService;

        public StudentsController(
            ILoggingBroker loggingBroker,
            IStudentEventOrchestrationService studentEventOrchestrationService)
        {
            this.loggingBroker = loggingBroker;
            this.studentEventOrchestrationService = studentEventOrchestrationService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Student>> PostStudentAsync(Student student) =>
            await Trace(
                function: async () =>
                    {
                        this.loggingBroker.LogTrace($"Adding student: {student.Id} - {student.Name} \n" +
                            $"ParentSpanId: {Activity.Current.ParentSpanId} \n" +
                            $"ParentId: {Activity.Current.ParentId} \n" +
                            $"SpanId: {Activity.Current.SpanId} \n" +
                            $"Id: {Activity.Current.Id} \n");

                        Student addedStudent =
                            await this.studentEventOrchestrationService.AddStudentAsync(student);

                        this.loggingBroker
                            .LogTrace($"Student added: {addedStudent.Id} - {student.Name}", Activity.Current);

                        return Created(addedStudent);
                    },
                activityName: $"CulDeSacDemoApi.StudentsController.PostStudentAsync",
                tags: new Dictionary<string, string> { { "StudentId", student.Id.ToString() } },
                baggage: new Dictionary<string, string> { { "StudentId", student.Id.ToString() } });
    }
}
