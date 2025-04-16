using MicroService.Models;
using MicroService.Services;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace MicroServiceTests
{
    public class MicroServiceTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(50)]
        public async void ChecksInShouldMatchChecksOut_Success(int numOfChecks)
        {
            // Arrange
            var microService = new MyService();

            // Act
            var actual = await microService.ExecuteRules(GetChecks(numOfChecks), new CancellationToken());

            // Assert
            actual.Should().HaveCount(numOfChecks);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(50)]
        public async void ChecksInShouldMatchChecksOut_Succesas(int numOfChecks)
        {
            // Arrange
            var microService = new MyService();

            // Act
            var actual = await microService.ExecuteRules(GetChecks(numOfChecks), new CancellationToken());

            // Assert
            for (int i = 0; i < numOfChecks; i++)
            {
                actual.Should().Contain(x => x.Index == i + 1);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(50)]
        public async void Should_Assert_That_Index_Values_Are_Unique(int numOfChecks)
        {
            // Arrange
            var microService = new MyService();
            
            // Act
            var actual = await microService.ExecuteRules(GetChecks(numOfChecks), new CancellationToken());
            var uniqueIndexes = actual.Select(obj => obj.Index).Distinct();

            // Assert
            actual.Count().Should().Be(uniqueIndexes.Count(), "All index values must be unique.");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(50)]
        public async void Should_Fail_If_Task_Did_Not_Complete_Within_3000_Milliseconds(int numOfChecks)
        {
            // Arrange
            var microService = new MyService();

            // Act
            var actualTask = () => microService.ExecuteRules(GetChecks(numOfChecks), new CancellationToken());


            // Assert
            await actualTask.Should().CompleteWithinAsync(3000.Milliseconds());
        }

        private static Request GetChecks(int v)
        {
            var request = new Request();
            for (int i = 0; i < v; i++)
            {
                request.Checks.Add(
                    new Check()
                    {
                        RoutingNumber = "123456789",
                        AccountNumber = "0912345678",
                        Amount = 100
                    });
            }
            return request;
        }
    }
}