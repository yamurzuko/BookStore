using BookStore.Application.AuthorOperations.UpdateAuthor;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.AuthorOperationTests.UpdateAuthorTests
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(" ", " canlı")]
        [InlineData("ug", "yagmur")]
        [InlineData("ugur", " ")]
        [InlineData("ugur", "yag")]

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);

            command.Model = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]

        public void WhenDateTimeNowIsGiven_Validator_ShouldBeReturnErrors()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);

            command.Model = new UpdateAuthorModel()
            {
                Name = "Uğur Can",
                Surname = "Yağmur",
                BirthDate = DateTime.Now.Date,
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]

        public void WhenInvalidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);

            command.AuthorId = 10;
            command.Model = new UpdateAuthorModel()
            {
                Name = "Uğur Can",
                Surname = "Yağmur",
                BirthDate = DateTime.Now.Date.AddYears(-3)
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

