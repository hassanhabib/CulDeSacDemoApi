using System.Threading.Tasks;
using CulDeSacApi.Brokers.Loggings;
using CulDeSacApi.Brokers.Storages;
using CulDeSacApi.Models.Students;

namespace CulDeSacApi.Services.Foundations.Students
{
    public partial class StudentService : IStudentService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IStorageBroker storageBroker;

        public StudentService(ILoggingBroker loggingBroker, IStorageBroker storageBroker)
        {
            this.loggingBroker = loggingBroker;
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Student> AddStudentAsync(Student student) =>
            await Trace(
                function: async () => { return await this.storageBroker.InsertStudentAsync(student); },
                activityName: $"CulDeSacDemoApi.StudentService.AddStudentAsync");
    }
}
