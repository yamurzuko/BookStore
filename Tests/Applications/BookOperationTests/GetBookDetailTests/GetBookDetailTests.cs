using AutoMapper;
using BookStore.Application.BookOperations.GetBookDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.BookOperationTests.GetBookDetailTests
{
    public class GetBookDetailTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;

        private readonly IMapper _mapper;

        public GetBookDetailTests(CommonTestFixture testFixture)
		{
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
		}

        [Fact]

        public void WhenBookIdIsGivenNotInDataBase_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
        }

        [Fact]

        public void WhenValidBookIdIsGiven_Book_ShouldBeGetting()
        {
            var book = new Book()
            {
                Title = "Kırk Haramiler",
                GenreId = 1,
                AuthorId = 1,
                PageCount = 456,
                PublishDate = new System.DateTime(2000, 09, 21)
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId = book.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            book = _context.Books.SingleOrDefault(x => x.Id == command.BookId);

            book.Should().NotBeNull();
        }
    }
}

