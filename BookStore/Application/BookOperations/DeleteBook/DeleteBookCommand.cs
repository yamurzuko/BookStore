using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDBContext _dbcontext;

        public int BookId { get; set; }

        public DeleteBookCommand(IBookStoreDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Id == BookId);

            if (book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }

            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
        }
    }
}

