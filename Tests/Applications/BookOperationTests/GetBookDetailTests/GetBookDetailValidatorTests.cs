using BookStore.Application.BookOperations.GetBookDetail;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.BookOperationTests.GetBookDetailTests
{
    public class GetBookDetailValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]

        public void WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.BookId = bookId;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(1)]

        public void WhenInvalidBookIdAreGiven_Validator_ShouldNotBeReturnErrors(int bookId)
        {
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.BookId = bookId;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

