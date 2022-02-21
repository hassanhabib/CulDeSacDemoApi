using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryAccounts;

namespace CulDeSacApi.Services.Orchestrations.LibraryAccounts
{
    public interface ILibraryAccountOrchestrationService
    {
        ValueTask<LibraryAccount> CreateLibraryAccountAsync(LibraryAccount libraryAccount);
    }
}
