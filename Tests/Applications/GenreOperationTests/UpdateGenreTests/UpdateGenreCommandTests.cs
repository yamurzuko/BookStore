using BookStore.Application.GenreOperations.UpdateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.GenreOperationTests.UpdateGenreTests
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]

        public void WhenAlreadyExistGenreIdIsGiven_InvalidOperationsException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı.");
        }

        [Fact]

        public void WhenValidGenreIdIsGiven_Genre_ShouldBeUpdating()
        {
            var genre = new Genre()
            {
                Name = "Action",
                IsActive = true
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = genre.Id;

            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "Romantics",
                IsActive = false
            };

            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            genre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            genre.IsActive.Should().Be(model.IsActive);
        }
    }
}

