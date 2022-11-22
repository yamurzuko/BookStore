using AutoMapper;
using BookStore.Application.GenreOperations.GetGenreDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.GenreOperationTests.GetGenreDetailTests
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {

        private readonly BookStoreDBContext _context;

        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]

        public void WhenGenreIdIsGivenNotInDataBase_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
        }

        [Fact]

        public void WhenValidGenreIdIsGiven_Genre_ShouldBeGetting()
        {
            var genre = new Genre()
            {
                Name = "Dram",
                IsActive = true
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            GetGenreDetailQuery command = new GetGenreDetailQuery(_context, _mapper);
            command.GenreId = genre.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            genre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);

            genre.Should().NotBeNull();
        }
    }
}

