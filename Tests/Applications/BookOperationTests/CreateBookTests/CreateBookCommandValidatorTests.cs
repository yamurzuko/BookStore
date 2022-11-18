using BookStore.Application.BookOperations.CreateBook;
using FluentAssertions;
using Tests.TestSetup;
using static BookStore.Application.BookOperations.CreateBook.CreateBookCommand;

namespace Tests.Applications.BookOperationTests
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
	{
		[Theory]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 1, 0, 0)]
        [InlineData("", 0, 1, 0)]
        [InlineData(" ", 0, 0, 10)]
        [InlineData("Lord Of The Rings", 0, 0, 0)]
        [InlineData("Lord Of The Rings", 1, 0, 0)]
        [InlineData("Lord Of", 0, 1, 0)]
        [InlineData("Lo", 100, 0, 1)]

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
		{
			// arrange = Hazırlık

			CreateBookCommand command = new CreateBookCommand(null, null);
			command.Model = new CreateBookModel()
			{
				Title = title,
				PageCount = pageCount,
				PublishDate = DateTime.Now.Date.AddYears(-1),
				AuthorId = authorId,
				GenreId = genreId
			};

			// act = Çalıştırma

			CreateBookCommandValidator validator = new CreateBookCommandValidator();
			var result = validator.Validate(command);

			// assert = Doğrulama

			result.Errors.Count.Should().BeGreaterThan(0);
		}

		[Fact]

		public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnErrors()
		{
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Ring",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                AuthorId = 1,
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

			result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]

        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Ring",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                AuthorId = 1,
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

