using System.Threading.Tasks;
using CulDeSacApi.Brokers.Loggings;
using CulDeSacApi.Brokers.Storages;
using CulDeSacApi.Models.LibraryAccounts;

namespace CulDeSacApi.Services.Foundations.LibraryAccounts
{
    public partial class LibraryAccountService : ILibraryAccountService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IStorageBroker storageBroker;

        public LibraryAccountService(ILoggingBroker loggingBroker, IStorageBroker storageBroker)
        {
            this.loggingBroker = loggingBroker;
            this.storageBroker = storageBroker;
        }

        public async ValueTask<LibraryAccount> AddLibraryAccountAsync(LibraryAccount libraryAccount) =>
            await Trace(
                function: async () => { return await this.storageBroker.InsertLibraryAccountAsync(libraryAccount); },
                activityName: $"CulDeSacDemoApi.LibraryAccountService.AddLibraryAccountAsync");
    }
}
