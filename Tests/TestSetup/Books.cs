using BookStore.DBOperations;
using BookStore.Entities;

namespace Tests.TestSetup
{
    public static class Books
	{
		public static void AddBooks(this BookStoreDBContext context)
		{
            context.Books.AddRange(

                new Book
                {
                    Title = "Lean Startup",
                    GenreId = 1,
                    AuthorId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(1997, 05, 24)
                },
                new Book
                {
                    Title = "Healand",
                    GenreId = 2,
                    AuthorId = 2,
                    PageCount = 240,
                    PublishDate = new DateTime(2002, 09, 12)
                },
                new Book
                {
                    Title = "Dune",
                    GenreId = 3,
                    AuthorId = 3,
                    PageCount = 300,
                    PublishDate = new DateTime(2001, 12, 21)
                }
            );
        }
	}
}

