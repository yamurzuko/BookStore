using BookStore.Application.BookOperations.UpdateBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;
using static BookStore.Application.BookOperations.UpdateBook.UpdateBookCommand;

namespace Tests.Applications.BookOperationTests.UpdateBookTests
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]

        public void WhenAlreadyExistBookIdIsGiven_InvalidOperationsException_ShouldBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
        }

        [Fact]

        public void WhenValidBookIdIsGiven_Book_ShouldBeUpdating()
        {
            var book = new Book()
            {
                Title = "Keloğlan",
                GenreId = 3,
                AuthorId = 3,
                PageCount = 416,
                PublishDate = new System.DateTime(1454, 01, 01)
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = book.Id;

            UpdateBookModel model = new UpdateBookModel()
            {
                Title = "AliVeliDeli",
                GenreId = 1,
                AuthorId = 2
            };

            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            book = _context.Books.SingleOrDefault(x => x.Id == command.BookId);

            book.Should().NotBeNull();
            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }
    }
}

