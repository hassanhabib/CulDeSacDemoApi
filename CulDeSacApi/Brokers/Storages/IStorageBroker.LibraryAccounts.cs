using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryAccounts;

namespace CulDeSacApi.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<LibraryAccount> InsertLibraryAccountAsync(LibraryAccount libraryAccount);
    }
}
