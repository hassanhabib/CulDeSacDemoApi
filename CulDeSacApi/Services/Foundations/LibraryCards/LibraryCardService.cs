using System.Threading.Tasks;
using CulDeSacApi.Brokers.Storages;
using CulDeSacApi.Models.LibraryCards;

namespace CulDeSacApi.Services.Foundations.LibraryCards
{
    public class LibraryCardService : ILibraryCardService
    {
        private readonly IStorageBroker storageBroker;

        public LibraryCardService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<LibraryCard> AddLibraryCardAsync(LibraryCard libraryCard)
        {
            throw new System.NotImplementedException();
        }
    }
}
