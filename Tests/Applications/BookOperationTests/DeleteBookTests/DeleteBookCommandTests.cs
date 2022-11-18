using BookStore.Application.BookOperations.DeleteBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.BookOperationTests.DeleteBookTests
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDBContext _context;

		public DeleteBookCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
		}

		[Fact]

		public void WhenAlreadyExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			DeleteBookCommand command = new DeleteBookCommand(_context);

			FluentActions
				.Invoking(() => command.Handle())
				.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
        }

		[Fact]

		public void WhenValidBookIdAreGiven_Book_ShouldBeDeleted()
		{
			var book = new Book()
			{
				Title = "Duner",
				GenreId = 2,
				AuthorId = 2,
				PageCount = 1002,
				PublishDate = new System.DateTime(1999, 06, 07)
			};

			_context.Books.Add(book);
			_context.SaveChanges();

			DeleteBookCommand command = new DeleteBookCommand(_context);

			command.BookId = book.Id;

			FluentActions.Invoking(() => command.Handle()).Invoke();

			book = _context.Books.SingleOrDefault(x => x.Id == command.BookId);

            book.Should().BeNull();
        }
    }
}

