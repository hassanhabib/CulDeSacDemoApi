using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryAccounts;
using CulDeSacApi.Models.LibraryCards;
using CulDeSacApi.Services.Foundations.LibraryAccounts;
using CulDeSacApi.Services.Foundations.LibraryCards;
using CulDeSacApi.Services.Orchestrations.LibraryAccounts;
using Moq;
using Tynamix.ObjectFiller;

namespace CulDeSacApi.Tests.Unit.Services.Orchestrations.LibraryAccounts
{
    public partial class LibraryAccountOrchestrationServiceTests
    {
        private readonly Mock<ILibraryAccountService> libraryAccountServiceMock;
        private readonly Mock<ILibraryCardService> libraryCardServiceMock;
        private readonly ILibraryAccountOrchestrationService libraryAccountOrchestrationService;

        public LibraryAccountOrchestrationServiceTests()
        {
            this.libraryAccountServiceMock = new Mock<ILibraryAccountService>();
            this.libraryCardServiceMock = new Mock<ILibraryCardService>();

            this.libraryAccountOrchestrationService = new LibraryAccountOrchestrationService(
                libraryAccountService: this.libraryAccountServiceMock.Object,
                libraryCardService: this.libraryCardServiceMock.Object);
        }

        private static Expression<Func<LibraryCard, bool>> SameLibraryCardAs(
            LibraryCard expectedLibraryCard)
        {
            return actualLibraryCard => 
                actualLibraryCard.LibraryAccountId == expectedLibraryCard.LibraryAccountId;
        }

        private static LibraryAccount CreateRandomLibraryAccount() =>
            CreateLibraryAccountFiller().Create();

        private static Filler<LibraryAccount> CreateLibraryAccountFiller() =>
            new Filler<LibraryAccount>();
    }
}
