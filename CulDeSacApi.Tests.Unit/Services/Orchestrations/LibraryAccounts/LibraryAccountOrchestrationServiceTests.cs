using System;
using System.Linq.Expressions;
using CulDeSacApi.Brokers.Loggings;
using CulDeSacApi.Models.LibraryAccounts;
using CulDeSacApi.Models.LibraryCards;
using CulDeSacApi.Services.Foundations.LibraryAccounts;
using CulDeSacApi.Services.Foundations.LibraryCards;
using CulDeSacApi.Services.Foundations.LocalStudentEvents;
using CulDeSacApi.Services.Orchestrations.LibraryAccounts;
using Moq;
using Tynamix.ObjectFiller;

namespace CulDeSacApi.Tests.Unit.Services.Orchestrations.LibraryAccounts
{
    public partial class LibraryAccountOrchestrationServiceTests
    {
        private readonly Mock<ILibraryAccountService> libraryAccountServiceMock;
        private readonly Mock<ILibraryCardService> libraryCardServiceMock;
        private readonly Mock<ILocalStudentEventService> localStudentEventServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILibraryAccountOrchestrationService libraryAccountOrchestrationService;

        public LibraryAccountOrchestrationServiceTests()
        {
            this.libraryAccountServiceMock = new Mock<ILibraryAccountService>(MockBehavior.Strict);
            this.libraryCardServiceMock = new Mock<ILibraryCardService>(MockBehavior.Strict);
            this.localStudentEventServiceMock = new Mock<ILocalStudentEventService>(MockBehavior.Strict);
            this.loggingBrokerMock = new Mock<ILoggingBroker>(MockBehavior.Strict);

            this.libraryAccountOrchestrationService = new LibraryAccountOrchestrationService(
                libraryAccountService: this.libraryAccountServiceMock.Object,
                libraryCardService: this.libraryCardServiceMock.Object,
                localStudentEventService: this.localStudentEventServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Expression<Func<LibraryCard, bool>> SameLibraryCardAs(
            LibraryCard expectedLibraryCard)
        {
            return actualLibraryCard =>
                actualLibraryCard.LibraryAccountId == expectedLibraryCard.LibraryAccountId
                && actualLibraryCard.Id != Guid.Empty;
        }

        private static LibraryAccount CreateRandomLibraryAccount() =>
            CreateLibraryAccountFiller().Create();

        private static Filler<LibraryAccount> CreateLibraryAccountFiller() =>
            new Filler<LibraryAccount>();
    }
}
