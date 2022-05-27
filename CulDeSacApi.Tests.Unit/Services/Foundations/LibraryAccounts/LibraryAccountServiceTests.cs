using CulDeSacApi.Brokers.Storages;
using CulDeSacApi.Models.LibraryAccounts;
using CulDeSacApi.Services.Foundations.LibraryAccounts;
using Moq;
using Tynamix.ObjectFiller;

namespace CulDeSacApi.Tests.Unit.Services.Foundations.LibraryAccounts
{
    public partial class LibraryAccountServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly ILibraryAccountService libraryAccountService;

        public LibraryAccountServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.libraryAccountService = new LibraryAccountService(
                storageBroker: storageBrokerMock.Object);
        }

        private static LibraryAccount CreateRandomLibraryAcocunt() =>
            CreateLibraryAccountFiller().Create();

        private static Filler<LibraryAccount> CreateLibraryAccountFiller() =>
            new Filler<LibraryAccount>();
    }
}
