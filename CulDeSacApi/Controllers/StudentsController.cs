using System.Threading.Tasks;
using CulDeSacApi.Models.Students;
using CulDeSacApi.Services.Foundations.Students;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace CulDeSacApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : RESTFulController
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService) =>
            this.studentService = studentService;

        [HttpPost]
        public async ValueTask<ActionResult<Student>> PostStudentAsync(Student student)
        {
            Student addedStudent = await this.studentService.AddStudentAsync(student);

            return Created(addedStudent);
        }
    }
}
