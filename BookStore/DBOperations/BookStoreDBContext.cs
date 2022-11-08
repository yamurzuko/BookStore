using Microsoft.EntityFrameworkCore;

namespace BookStore.DBOperations
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}

