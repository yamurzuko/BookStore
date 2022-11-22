using BookStore.Application.AuthorOperations.DeleteAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.AuthorOperationTests.DeleteAuthorTests
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDBContext _context;

		public DeleteAuthorCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
		}

		[Fact]

		public void WhenAlreadyExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			DeleteAuthorCommand command = new DeleteAuthorCommand(_context);

			FluentActions
				.Invoking(() => command.Handle())
				.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı");
		}

        [Fact]

        public void WhenValidAuthorIdAreGiven_Author_ShouldBeDeleted()
        {
            var author = new Author()
            {
                Name = "Uğur",
                Surname = "Can",
                BirthDate = new System.DateTime(1897, 11, 05)
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);

            command.AuthorId = author.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            author = _context.Authors.SingleOrDefault(x => x.Id == command.AuthorId);

            author.Should().BeNull();
        }
    }
}

