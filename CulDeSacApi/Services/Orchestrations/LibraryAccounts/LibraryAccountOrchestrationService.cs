using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CulDeSacApi.Brokers.Loggings;
using CulDeSacApi.Models.LibraryAccounts;
using CulDeSacApi.Models.LibraryCards;
using CulDeSacApi.Services.Foundations.LibraryAccounts;
using CulDeSacApi.Services.Foundations.LibraryCards;
using CulDeSacApi.Services.Foundations.LocalStudentEvents;

namespace CulDeSacApi.Services.Orchestrations.LibraryAccounts
{
    public partial class LibraryAccountOrchestrationService : ILibraryAccountOrchestrationService
    {
        private readonly ILibraryAccountService libraryAccountService;
        private readonly ILibraryCardService libraryCardService;
        private readonly ILocalStudentEventService localStudentEventService;
        private readonly ILoggingBroker loggingBroker;

        public LibraryAccountOrchestrationService(
            ILibraryAccountService libraryAccountService,
            ILibraryCardService libraryCardService,
            ILocalStudentEventService localStudentEventService,
            ILoggingBroker loggingBroker)
        {
            this.libraryAccountService = libraryAccountService;
            this.libraryCardService = libraryCardService;
            this.localStudentEventService = localStudentEventService;
            this.loggingBroker = loggingBroker;
        }

        public void ListenToLocalStudentEvent() =>
            Trace(
                function: () =>
                {
                    this.localStudentEventService.ListenToStudentEvent(async (student) =>
                    {
                        var libraryAccount = new LibraryAccount
                        {
                            Id = Guid.NewGuid(),
                            StudentId = student.Id
                        };

                        await CreateLibraryAccountAsync(libraryAccount);

                        return student;
                    });
                },
                activityName: $"CulDeSacDemoApi.LibraryAccountOrchestrationService.ListenToLocalStudentEvent");

        public async ValueTask<LibraryAccount> CreateLibraryAccountAsync(LibraryAccount libraryAccount) =>
            await Trace(
                function: async () =>
                    {
                        LibraryAccount addedLibraryAccount =
                            await this.libraryAccountService
                                .AddLibraryAccountAsync(libraryAccount);

                        this.loggingBroker
                            .LogTrace($"Library Account added: {libraryAccount.Id}", Activity.Current);

                        await CreateLibraryCardAsync(libraryAccount);

                        return addedLibraryAccount;
                    },
                activityName: $"CulDeSacDemoApi.LibraryAccountOrchestrationService.CreateLibraryAccountAsync",
                tags: new Dictionary<string, string> { { "LibraryAccountId", libraryAccount.Id.ToString() } },
                baggage: new Dictionary<string, string> { { "LibraryAccountId", libraryAccount.Id.ToString() } });

        private async Task CreateLibraryCardAsync(LibraryAccount libraryAccount)
        {
            LibraryCard inputLibraryCard =
                CreateLibraryCard(libraryAccount.Id);

            await this.libraryCardService
                .AddLibraryCardAsync(inputLibraryCard);

            this.loggingBroker.LogTrace($"Library Card added: {inputLibraryCard.Id}", Activity.Current);
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
