using System.Threading.Tasks;
using CulDeSacApi.Brokers.Loggings;
using CulDeSacApi.Brokers.Storages;
using CulDeSacApi.Models.LibraryCards;

namespace CulDeSacApi.Services.Foundations.LibraryCards
{
    public partial class LibraryCardService : ILibraryCardService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IStorageBroker storageBroker;

        public LibraryCardService(ILoggingBroker loggingBroker, IStorageBroker storageBroker)
        {
            this.loggingBroker = loggingBroker;
            this.storageBroker = storageBroker;
        }

        public async ValueTask<LibraryCard> AddLibraryCardAsync(LibraryCard libraryCard) =>
            await Trace(
                function: async () => { return await this.storageBroker.InsertLibraryCardAsync(libraryCard); },
                activityName: $"CulDeSacDemoApi.LibraryCardService.AddLibraryCardAsync");
    }
}
