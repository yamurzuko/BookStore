using AutoMapper;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }

        private readonly IBookStoreDBContext _dbContext;

        private readonly IMapper _dbMapper;

        public GetBookDetailQuery(IBookStoreDBContext dbContext, IMapper dbMapper)
        {
            _dbContext = dbContext;
            _dbMapper = dbMapper;

        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).SingleOrDefault(book => book.Id == BookId);

            if(book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            /*
            BookDetailViewModel viewModel = new BookDetailViewModel();
            viewModel.Title = book.Title;
            viewModel.Genre = ((GenreEnum)book.GenreId).ToString();
            viewModel.PageCount = book.PageCount;
            viewModel.PublishDate = book.PublishDate.Date.ToString("dd/mm/yyy");
            */

            BookDetailViewModel viewModel = _dbMapper.Map<BookDetailViewModel>(book);

            return viewModel;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }

        public string Genre { get; set; }

        public string Author { get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }
    }
}

