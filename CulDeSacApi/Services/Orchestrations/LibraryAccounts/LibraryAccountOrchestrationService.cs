using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryAccounts;
using CulDeSacApi.Services.Foundations.LibraryAccounts;
using CulDeSacApi.Services.Foundations.LibraryCards;

namespace CulDeSacApi.Services.Orchestrations.LibraryAccounts
{
    public class LibraryAccountOrchestrationService : ILibraryAccountOrchestrationService
    {
        private readonly ILibraryAccountService libraryAccountService;
        private readonly ILibraryCardService libraryCardService;

        public LibraryAccountOrchestrationService(
            ILibraryAccountService libraryAccountService,
            ILibraryCardService libraryCardService)
        {
            this.libraryAccountService = libraryAccountService;
            this.libraryCardService = libraryCardService;
        }

        public ValueTask<LibraryAccount> CreateLibraryAccountAsync(LibraryAccount libraryAccount)
        {
            throw new System.NotImplementedException();
        }
    }
}
