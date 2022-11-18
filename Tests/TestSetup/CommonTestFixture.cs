using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace Tests.TestSetup
{
    public class CommonTestFixture
	{
        public BookStoreDBContext Context { get; set; }

        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDBContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;

            Context = new BookStoreDBContext(options);
            Context.Database.EnsureCreated();

            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
	}
}

