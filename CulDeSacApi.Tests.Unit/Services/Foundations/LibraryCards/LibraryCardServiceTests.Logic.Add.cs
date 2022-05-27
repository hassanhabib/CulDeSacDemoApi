using System.Threading.Tasks;
using CulDeSacApi.Models.LibraryCards;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace CulDeSacApi.Tests.Unit.Services.Foundations.LibraryCards
{
    public partial class LibraryCardServiceTests
    {
        [Fact]
        public async Task ShouldAddLibraryCardAsync()
        {
            // given
            LibraryCard randomLibraryCard =
                CreateRandomLibraryAcocunt();

            LibraryCard inputLibraryCard =
                randomLibraryCard;

            LibraryCard insertedLibraryCard =
                inputLibraryCard;

            LibraryCard expectedLibraryCard =
                insertedLibraryCard.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertLibraryCardAsync(inputLibraryCard))
                    .ReturnsAsync(insertedLibraryCard);

            // when
            LibraryCard actualLibraryCard =
                await this.libraryCardService.AddLibraryCardAsync(
                    inputLibraryCard);

            // then
            actualLibraryCard.Should().BeEquivalentTo(actualLibraryCard);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertLibraryCardAsync(inputLibraryCard),
                    Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
