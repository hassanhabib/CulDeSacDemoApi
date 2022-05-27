using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.Students;

namespace CulDeSacApi.Services.Orchestrations.StudentEvents
{
    public interface IStudentEventOrchestrationService
    {
        void ListenToStudentEvents();
    }
}
