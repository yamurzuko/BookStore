using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDBContext _dbContext;

        public int BookId { get; set; }

        public GetBookDetailQuery(BookStoreDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();

            if(book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }

            BookDetailViewModel viewModel = new BookDetailViewModel();
            viewModel.Title = book.Title;
            viewModel.Genre = ((GenreEnum)book.GenreId).ToString();
            viewModel.PageCount = book.PageCount;
            viewModel.PublishDate = book.PublishDate.Date.ToString("dd/mm/yyy");

            return viewModel;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }

        public string Genre { get; set; }

        public int PageCount { get; set; }

        public string PublishDate { get; set; }
    }
}

