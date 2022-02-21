using CulDeSacApi.Models.LibraryCards;
using Microsoft.EntityFrameworkCore;

namespace CulDeSacApi.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void AddLibraryCardsReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryCard>()
                .HasOne(libraryCard => libraryCard.LibraryAccount)
                .WithMany(account => account.LibraryCards)
                .HasForeignKey(libraryCard => libraryCard.LibraryAccountId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
