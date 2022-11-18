using AutoMapper;
using BookStore.Application.AuthorOperations.CreateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using Tests.TestSetup;

namespace Tests.Applications.AuthorOperationTests.CreateAuthorTests
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
	{
		private readonly BookStoreDBContext _context;

		private readonly IMapper _mapper;

		public CreateAuthorCommandTests(CommonTestFixture testFixture)
		{
			_context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		[Fact]

		public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			var author = new Author()
			{
				Name = "WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",
				Surname = "Yağmur",
				BirthDate = new System.DateTime(1997, 05, 24)
			};

			_context.Authors.Add(author);
			_context.SaveChanges();

			CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
			command.Model = new CreateAuthorModel()
			{
				Name = author.Name
			};

			FluentActions
				.Invoking(() => command.Handle())
				.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
		}

		[Fact]

        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                Name = "Uğur Can",
                Surname = "Yağmur",
                BirthDate = new System.DateTime(1990, 04, 04)
            };

            command.Model = model;

            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(x => x.Name == model.Name);

            author.Should().NotBeNull();
            author.Surname.Should().Be(model.Surname);
            author.BirthDate.Should().Be(model.BirthDate);

        }
    }
}

