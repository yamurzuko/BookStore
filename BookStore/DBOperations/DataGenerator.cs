using Microsoft.EntityFrameworkCore;
using BookStore.Entities;

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

                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Orhan",
                        Surname = "Veli",
                        BirthDate = new DateTime(1990, 10, 05)
                    },
                    new Author
                    {
                        Name = "Ahmet",
                        Surname = "Ucar",
                        BirthDate = new DateTime(1960, 11, 24)
                    },
                    new Author
                    {
                        Name = "Tuba",
                        Surname = "Güler",
                        BirthDate = new DateTime(1972, 08, 08)
                    }
                );

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

                context.SaveChanges();
            }
        }
    }
}

