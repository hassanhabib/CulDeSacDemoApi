using System.Diagnostics;
using CulDeSacApi.Brokers.Loggings;
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
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILibraryAccountService libraryAccountService;

        public LibraryAccountServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            var activityListener = new ActivityListener
            {
                ShouldListenTo = s => true,
                SampleUsingParentId = (ref ActivityCreationOptions<string> activityOptions) => ActivitySamplingResult.AllData,
                Sample = (ref ActivityCreationOptions<ActivityContext> activityOptions) => ActivitySamplingResult.AllData
            };

            this.libraryAccountService = new LibraryAccountService(
                storageBroker: storageBrokerMock.Object,
                loggingBroker: loggingBrokerMock.Object);
        }

        private static LibraryAccount CreateRandomLibraryAcocunt() =>
            CreateLibraryAccountFiller().Create();

        private static Filler<LibraryAccount> CreateLibraryAccountFiller() =>
            new Filler<LibraryAccount>();
    }
}
