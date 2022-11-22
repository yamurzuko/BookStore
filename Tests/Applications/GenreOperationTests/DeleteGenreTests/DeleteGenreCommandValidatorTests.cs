using BookStore.Application.GenreOperations.DeleteGenre;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.GenreOperationTests.DeleteGenreTests
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]

        public void WhenInvalidGenreIdAreGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(1)]

        public void WhenInvalidGenreIdAreGiven_Validator_ShouldNotBeReturnErrors(int genreId)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

