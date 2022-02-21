using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryCards;

namespace CulDeSacApi.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<LibraryCard> InsertLibraryCardAsync(LibraryCard libraryCard);
    }
}
