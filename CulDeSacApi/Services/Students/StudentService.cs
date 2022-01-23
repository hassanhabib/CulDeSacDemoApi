using System.Threading.Tasks;
using CulDeSacApi.Brokers.Storages;
using CulDeSacApi.Models.Students;

namespace CulDeSacApi.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly IStorageBroker storageBroker;

        public StudentService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<Student> AddStudentAsync(Student student) => 
            throw new System.NotImplementedException();
    }
}
