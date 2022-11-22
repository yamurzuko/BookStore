using AutoMapper;
using BookStore.Application.GenreOperations.CreateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;
using static BookStore.Application.GenreOperations.CreateGenre.CreateGenreCommand;

namespace Tests.Applications.GenreOperationTests.CreateGenreTests
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {

        private readonly BookStoreDBContext _context;

        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]

        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre()
            {
                Name = "WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",
                IsActive = true
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel()
            {
                Name = genre.Name
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Zaten Mevcut");
        }

        [Fact]

        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            CreateGenreModel model = new CreateGenreModel()
            {
                Name = "Uğur Can"
            };

            command.Model = model;

            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            var author = _context.Genres.SingleOrDefault(x => x.Name == model.Name);

            author.Should().NotBeNull();

        }
    }
}

