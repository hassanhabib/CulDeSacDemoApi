using System;
using CulDeSacApi.Models.LibraryAccounts;

namespace CulDeSacApi.Models.LibraryCards
{
    public class LibraryCard
    {
        public Guid Id { get; set; }

        public Guid LibraryAccountId { get; set; }
        public LibraryAccount LibraryAccount { get; set; }
    }
}
