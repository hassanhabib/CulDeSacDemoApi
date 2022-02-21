using CulDeSacApi.Models.LibraryAccounts;
using Microsoft.EntityFrameworkCore;

namespace CulDeSacApi.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<LibraryAccount> LibraryAccounts { get; set; }
    }
}
