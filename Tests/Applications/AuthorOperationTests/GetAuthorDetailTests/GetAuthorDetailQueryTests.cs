using AutoMapper;
using BookStore.Application.AuthorOperations.GetAuthorDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.AuthorOperationTests.GetAuthorDetailTests
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;

        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]

        public void WhenAuthorIdIsGivenNotInDataBase_InvalidOperationException_ShouldBeReturn()
        {
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı");
        }

        [Fact]

        public void WhenValidAuthorIdIsGiven_Author_ShouldBeGetting()
        {
            var author = new Author()
            {
                Name = "ugur",
                Surname = "can",
                BirthDate = new System.DateTime(1788, 11, 08)
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
            command.AuthorId = author.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            author = _context.Authors.SingleOrDefault(x => x.Id == command.AuthorId);

            author.Should().NotBeNull();
        }
    }
}

