using BookStore.Application.GenreOperations.CreateGenre;
using FluentAssertions;
using Tests.TestSetup;
using static BookStore.Application.GenreOperations.CreateGenre.CreateGenreCommand;

namespace Tests.Applications.GenreOperationTests.CreateGenreTests
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Fact]

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors()
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel()
            {
                Name = "dra"
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]

        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.Model = new CreateGenreModel()
            {
                Name = "Dram"
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

