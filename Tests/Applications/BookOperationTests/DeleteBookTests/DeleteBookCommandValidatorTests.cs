using BookStore.Application.BookOperations.DeleteBook;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.BookOperationTests.DeleteBookTests
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
	{

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]

		public void WhenInvalidBookIdAreGiven_Validator_ShouldBeReturnErrors(int bookId)
		{
			DeleteBookCommand command = new DeleteBookCommand(null);
			command.BookId = bookId;

			DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
			var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

		[Theory]
		[InlineData(10)]
        [InlineData(1)]

        public void WhenInvalidBookIdAreGiven_Validator_ShouldNotBeReturnErrors(int bookId)
		{
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

