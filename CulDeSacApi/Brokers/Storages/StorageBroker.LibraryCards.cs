using CulDeSacApi.Models.LibraryCards;
using Microsoft.EntityFrameworkCore;

namespace CulDeSacApi.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<LibraryCard> LibraryCards { get; set; }
    }
}
