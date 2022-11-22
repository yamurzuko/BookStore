using BookStore.Application.AuthorOperations.UpdateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.AuthorOperationTests.UpdateAuthorTests
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDBContext _context;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]

        public void WhenAlreadyExistAuthorIdIsGiven_InvalidOperationsException_ShouldBeReturn()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı");
        }

        [Fact]

        public void WhenValidAuthorIdIsGiven_Author_ShouldBeUpdating()
        {
            var author = new Author()
            {
                Name = "ugur",
                Surname = "can",
                BirthDate = new System.DateTime(1172, 11, 11)
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = author.Id;

            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                Name = "AliVeliDeli",
                Surname = "deligeliveri",
                BirthDate = new System.DateTime(1834, 03, 07)
            };

            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            author = _context.Authors.SingleOrDefault(x => x.Id == command.AuthorId);

            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.BirthDate.Should().Be(model.BirthDate);
        }
    }
}

