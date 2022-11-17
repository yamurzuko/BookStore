using BookStore.DBOperations;
using AutoMapper;
using BookStore.Entities;


namespace BookStore.Application.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }

        public UpdateBookModel Model { get; set; }

        private readonly IBookStoreDBContext _dbContext;

        public UpdateBookCommand(IBookStoreDBContext dBContext)
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
            book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;

            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }

            public int GenreId { get; set; }

            public int AuthorId { get; set; }
        }
    }
}

