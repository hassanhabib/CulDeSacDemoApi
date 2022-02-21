using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryCards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CulDeSacApi.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<LibraryCard> LibraryCards { get; set; }

        public async ValueTask<LibraryCard> InsertLibraryCardAsync(LibraryCard libraryCard)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<LibraryCard> libraryCardEntityEntry =
                await broker.LibraryCards.AddAsync(libraryCard);

            await broker.SaveChangesAsync();

            return libraryCardEntityEntry.Entity;
        }
    }
}
