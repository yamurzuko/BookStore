using BookStore.DBOperations;
using BookStore.Entities;

namespace Tests.TestSetup
{
    public static class Genres
    {
		public static void AddGenres(this BookStoreDBContext context)
		{
            context.Genres.AddRange(

                new Genre
                {
                    Name = "Personal Growt"
                },
                new Genre
                {
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Name = "Romance"
                }
            );
        }
	}
}

