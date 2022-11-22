using BookStore.Application.GenreOperations.UpdateGenre;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.GenreOperationTests.UpdateGenreTests
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Fact]

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);

            command.Model = new UpdateGenreModel()
            {
                Name = "asd",
                IsActive = true
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]

        public void WhenInvalidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);

            command.GenreId = 10;
            command.Model = new UpdateGenreModel()
            {
                Name = "bilimkurgu",
                IsActive = true
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

