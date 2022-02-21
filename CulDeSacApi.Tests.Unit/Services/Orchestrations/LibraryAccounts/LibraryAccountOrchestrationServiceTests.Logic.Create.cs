using System;
using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryAccounts;
using CulDeSacApi.Models.LibraryCards;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CulDeSacApi.Tests.Unit.Services.Orchestrations.LibraryAccounts
{
    public partial class LibraryAccountOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldCreateLibraryAccountAsync()
        {
            // given
            LibraryAccount randomLibraryAccount =
                CreateRandomLibraryAccount();

            LibraryAccount inputLibraryAccount =
                randomLibraryAccount;

            LibraryAccount addedLibraryAccount =
                inputLibraryAccount;

            LibraryAccount expectedLibraryAccount =
                addedLibraryAccount.DeepClone();

            var expectedInputLibraryCard = new LibraryCard
            {
                Id = Guid.NewGuid(),
                LibraryAccountId = addedLibraryAccount.Id
            };

            this.libraryAccountServiceMock.Setup(service =>
                service.AddLibraryAccountAsync(inputLibraryAccount))
                    .ReturnsAsync(addedLibraryAccount);

            // when
            LibraryAccount actualLibraryAccount =
                await this.libraryAccountOrchestrationService
                    .CreateLibraryAccountAsync(inputLibraryAccount);

            // then
            actualLibraryAccount.Should().BeEquivalentTo(expectedLibraryAccount);

            this.libraryAccountServiceMock.Verify(service =>
                service.AddLibraryAccountAsync(inputLibraryAccount),
                    Times.Once);

            this.libraryCardServiceMock.Verify(broker =>
                broker.AddLibraryCardAsync(It.Is(SameLibraryCardAs(
                    expectedInputLibraryCard))),
                        Times.Once);

            this.libraryAccountServiceMock.VerifyNoOtherCalls();
            this.libraryCardServiceMock.VerifyNoOtherCalls();
        }
    }
}
