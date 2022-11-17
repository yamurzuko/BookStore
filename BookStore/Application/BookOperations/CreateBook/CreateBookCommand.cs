using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }

        private readonly IBookStoreDBContext _dbContect;

        private readonly IMapper _dbMapper;

        public CreateBookCommand(IBookStoreDBContext dbContext, IMapper dbMapper)
        {
            _dbContect = dbContext;
            _dbMapper = dbMapper;
        }

        public void Handle()
        {
            var book = _dbContect.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book != null)
            {
                throw new InvalidOperationException("Kitap Zaten Mevcut");
            }

            /*
            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;
            */

            book = _dbMapper.Map<Book>(Model);

            _dbContect.Books.Add(book);
            _dbContect.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }

            public int GenreId { get; set; }

            public int AuthorId { get; set; }

            public int PageCount { get; set; }

            public DateTime PublishDate { get; set; }
        }
    }
}

