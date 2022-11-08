using Microsoft.EntityFrameworkCore;

namespace BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(

                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1, 
                        PageCount = 200,
                        PublishDate = new DateTime(1997, 05, 24)
                    },
                    new Book
                    {
                        Title = "Healand",
                        GenreId = 2, 
                        PageCount = 240,
                        PublishDate = new DateTime(2002, 09, 12)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 3,
                        PageCount = 300,
                        PublishDate = new DateTime(2001, 12, 21)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}

