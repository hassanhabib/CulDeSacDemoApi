using System;
using System.Collections.Generic;
using CulDeSacApi.Models.LibraryCards;
using CulDeSacApi.Models.Students;

namespace CulDeSacApi.Models.LibraryAccounts
{
    public class LibraryAccount
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public IEnumerable<LibraryCard> LibraryCards { get; set; }
    }
}
