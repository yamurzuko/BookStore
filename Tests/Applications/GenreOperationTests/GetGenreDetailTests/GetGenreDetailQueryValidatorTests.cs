using BookStore.Application.GenreOperations.GetGenreDetail;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.GenreOperationTests.GetGenreDetailTests
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]

        public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(null, null);
            command.GenreId = genreId;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(1)]

        public void WhenInvalidGenreIdAreGiven_Validator_ShouldNotBeReturnErrors(int genreId)
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(null, null);
            command.GenreId = genreId;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

