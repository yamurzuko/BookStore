using BookStore.Application.AuthorOperations.CreateAuthor;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.AuthorOperationTests.CreateAuthorTests
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Theory]
		[InlineData(" ", " ")]
        [InlineData("ug", "yag")]
        [InlineData("ugurca", "yag")]
        [InlineData("ugu", "yagmur")]

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
		{
			CreateAuthorCommand command = new CreateAuthorCommand(null, null);
			command.Model = new CreateAuthorModel()
			{
				Name = name,
				Surname = surname,
                BirthDate = DateTime.Now.Date.AddYears(-1),
            };

			CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
			var result = validator.Validate(command);

			result.Errors.Count.Should().BeGreaterThan(0);
		}

        [Fact]

        public void WhenDateTimeNowIsGiven_Validator_ShouldBeReturnErrors()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "UğurCan",
                Surname = "Yağmur",
                BirthDate = DateTime.Now.Date,
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]

        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel()
            {
                Name = "Uğur Can",
                Surname = "Yağmur",
                BirthDate = DateTime.Now.Date.AddYears(-2)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

