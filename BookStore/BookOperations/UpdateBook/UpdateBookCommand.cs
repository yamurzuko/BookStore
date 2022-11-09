using BookStore.DBOperations;
using AutoMapper;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }

        public UpdateBookModel Model { get; set; }

        private readonly BookStoreDBContext _dbContext;

        public UpdateBookCommand(BookStoreDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if (book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }

            public int GenreId { get; set; }
        }
    }
}

