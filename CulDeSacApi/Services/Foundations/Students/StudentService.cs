using System.Threading.Tasks;
using CulDeSacApi.Brokers.Storages;
using CulDeSacApi.Models.Students;

namespace CulDeSacApi.Services.Foundations.Students
{
    public class StudentService : IStudentService
    {
        private readonly IStorageBroker storageBroker;

        public StudentService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Student> AddStudentAsync(Student student) => 
            await this.storageBroker.InsertStudentAsync(student);
    }
}
