using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryAccounts;
using CulDeSacApi.Models.LibraryCards;
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

        public async ValueTask<LibraryAccount> CreateLibraryAccountAsync(LibraryAccount libraryAccount)
        {
            LibraryAccount addedLibraryAccount =
                await this.libraryAccountService
                    .AddLibraryAccountAsync(libraryAccount);

            await CreateLibraryCardAsync(libraryAccount);

            return addedLibraryAccount;
        }

        private async Task CreateLibraryCardAsync(LibraryAccount libraryAccount)
        {
            LibraryCard inputLibraryCard =
                CreateLibraryCard(libraryAccount.Id);

            await this.libraryCardService
                .AddLibraryCardAsync(inputLibraryCard);
        }

        private static LibraryCard CreateLibraryCard(Guid libraryAccountId)
        {
            return new LibraryCard
            {
                Id = Guid.NewGuid(),
                LibraryAccountId = libraryAccountId
            };
        }
    }
}
