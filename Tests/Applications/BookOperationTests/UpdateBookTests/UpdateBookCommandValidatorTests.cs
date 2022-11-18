using BookStore.Application.BookOperations.UpdateBook;
using FluentAssertions;
using Tests.TestSetup;
using static BookStore.Application.BookOperations.UpdateBook.UpdateBookCommand;

namespace Tests.Applications.BookOperationTests.UpdateBookTests
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Theory]
		[InlineData(" ", 0, 0)]
        [InlineData(" ", 1, 1)]
        [InlineData("As", 1, 1)]
        [InlineData("as", 0, 0)]
        [InlineData("asdfs", 0, 1)]
        [InlineData("asddajsd", 1, 0)]
        [InlineData("asdfsskd", 0, 0)]
        
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId, int authorId)
		{
			UpdateBookCommand command = new UpdateBookCommand(null);

			command.Model = new UpdateBookModel()
			{
				Title = title,
				GenreId = genreId,
				AuthorId = authorId
			};

			UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
			var result = validator.Validate(command);

			result.Errors.Count.Should().BeGreaterThan(0);
        }

		[Fact]

		public void WhenInvalidInputAreGiven_Validator_ShouldNotBeReturnErrors()
		{
			UpdateBookCommand command = new UpdateBookCommand(null);

			command.BookId = 10;
			command.Model = new UpdateBookModel()
			{
				Title = "Keloğlan",
				AuthorId = 2,
				GenreId = 1
			};

			UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
			var result = validator.Validate(command);

			result.Errors.Count.Should().Be(0);
		}

    }
}

