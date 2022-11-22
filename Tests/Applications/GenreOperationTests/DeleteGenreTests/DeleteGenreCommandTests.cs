using BookStore.Application.GenreOperations.DeleteGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.GenreOperationTests.DeleteGenreTests
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]

        public void WhenAlreadyExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
        }

        [Fact]

        public void WhenValidGenreIdAreGiven_Genre_ShouldBeDeleted()
        {
            var genre = new Genre()
            {
                Name = "Dram",
                IsActive = true
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            DeleteGenreCommand command = new DeleteGenreCommand(_context);

            command.GenreId = genre.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            genre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);

            genre.Should().BeNull();
        }
    }
}

